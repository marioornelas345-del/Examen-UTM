using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FoodCampus.Application.UseCases;
using FoodCampus.Application.Persistence.Repositories;
using FoodCampus.Application.DTOs;
using FoodCampus.Domain.Extensions;

namespace FoodCampus.API;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("CRÍTICO: No se pudo leer la cadena de conexión de appsettings.json");
            Console.ResetColor();
            return;
        }
        Console.WriteLine($"[Config] Conexión cargada: {connectionString.Split(';')[0]}...");

        var services = new ServiceCollection();

        // Application Services
        services.AddScoped<IRestauranteUseCase, RestauranteService>();
        services.AddScoped<IPedidoUseCase, PedidoService>();
        services.AddScoped<IPlatilloUseCase, PlatilloService>();

        // Infrastructure - Using Reflection to avoid direct reference
        try 
        {
            var infraDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FoodCampus.Infrastructure.dll");
            if (File.Exists(infraDllPath))
            {
                var infraAssembly = Assembly.LoadFrom(infraDllPath);
                var restauranteRepoType = infraAssembly.GetType("FoodCampus.Infrastructure.Persistence.Repositories.DapperRestauranteRepository");
                var pedidoRepoType = infraAssembly.GetType("FoodCampus.Infrastructure.Persistence.Repositories.DapperPedidoRepository");
                var platilloRepoType = infraAssembly.GetType("FoodCampus.Infrastructure.Persistence.Repositories.DapperPlatilloRepository");

                if (restauranteRepoType != null)
                    services.AddScoped(typeof(IRestauranteRepository), sp => Activator.CreateInstance(restauranteRepoType, connectionString)!);
                
                if (pedidoRepoType != null)
                    services.AddScoped(typeof(IPedidoRepository), sp => Activator.CreateInstance(pedidoRepoType, connectionString)!);

                if (platilloRepoType != null)
                    services.AddScoped(typeof(IPlatilloRepository), sp => Activator.CreateInstance(platilloRepoType, connectionString)!);
                
                // Requirement 4.3: Unbound generics with nameof (C# 14 feature)
                string genericName = nameof(IRepository<>); 
                Console.WriteLine($"[DI Config] Registrando repositorio genérico: {genericName}");
            }
            else
            {
                throw new FileNotFoundException($"No se encontró FoodCampus.Infrastructure.dll en {AppDomain.CurrentDomain.BaseDirectory}. Asegúrese de copiarla para el despliegue.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error al cargar Infrastructure: {ex.Message}");
            Console.ResetColor();
        }

        var serviceProvider = services.BuildServiceProvider();

        await RunMenu(serviceProvider);
    }

    static async Task RunMenu(ServiceProvider sp)
    {
        var restauranteUseCase = sp.GetRequiredService<IRestauranteUseCase>();
        var pedidoUseCase = sp.GetRequiredService<IPedidoUseCase>();
        var platilloUseCase = sp.GetRequiredService<IPlatilloUseCase>();

        while (true)
        {
            Console.WriteLine("\n--- FoodCampus Delivery ---");
            Console.WriteLine("1. Registrar Restaurante");
            Console.WriteLine("2. Consultar Restaurantes");
            Console.WriteLine("3. Registrar Platillo al Menú");
            Console.WriteLine("4. Consultar Menú por Restaurante");
            Console.WriteLine("5. Registrar Pedido");
            Console.WriteLine("6. Consultar Pedidos por Usuario");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");

            string? opcion = Console.ReadLine();
            if (opcion == "7") break;

            try
            {
                switch (opcion)
                {
                    case "1":
                        await RegistrarRestaurante(restauranteUseCase);
                        break;
                    case "2":
                        await ConsultarRestaurantes(restauranteUseCase);
                        break;
                    case "3":
                        await RegistrarPlatillo(platilloUseCase);
                        break;
                    case "4":
                        await ConsultarMenu(platilloUseCase);
                        break;
                    case "5":
                        await RegistrarPedido(pedidoUseCase);
                        break;
                    case "6":
                        await ConsultarPedidos(pedidoUseCase);
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    static async Task ConsultarMenu(IPlatilloUseCase useCase)
    {
        Console.Write("ID del Restaurante: ");
        if (!int.TryParse(Console.ReadLine(), out var idRest)) return;

        var platillos = await useCase.GetByRestauranteAsync(idRest);
        Console.WriteLine($"\n--- Menú del Restaurante {idRest} ---");
        foreach (var p in platillos)
        {
            Console.WriteLine($"{p.Id}. {p.Nombre} - {p.Precio:C}");
        }
    }

    static async Task RegistrarPlatillo(IPlatilloUseCase useCase)
    {
        Console.Write("Nombre del Platillo: ");
        string nombre = Console.ReadLine() ?? "";
        
        Console.Write("Precio: ");
        if (!decimal.TryParse(Console.ReadLine(), out var precio)) precio = 0;

        Console.Write("ID del Restaurante: ");
        if (!int.TryParse(Console.ReadLine(), out var idRest)) idRest = 1;

        await useCase.RegisterAsync(new CreatePlatilloDto(nombre, precio, idRest));
        Console.WriteLine("Platillo registrado con éxito.");
    }

    static async Task RegistrarRestaurante(IRestauranteUseCase useCase)
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";
        Console.Write("Especialidad: ");
        string especialidad = Console.ReadLine() ?? "";
        
        Console.Write("Horario Apertura (HH:mm): ");
        if (!TimeOnly.TryParse(Console.ReadLine(), out var apertura)) apertura = new TimeOnly(8, 0);
        
        Console.Write("Horario Cierre (HH:mm): ");
        if (!TimeOnly.TryParse(Console.ReadLine(), out var cierre)) cierre = new TimeOnly(20, 0);

        await useCase.RegisterAsync(new CreateRestauranteDto(nombre, especialidad, apertura, cierre));
        Console.WriteLine("Restaurante registrado con éxito.");
    }

    static async Task ConsultarRestaurantes(IRestauranteUseCase useCase)
    {
        var lista = await useCase.GetAllAsync();
        Console.WriteLine("\n--- Lista de Restaurantes ---");
        foreach (var r in lista)
        {
            // Requirement 4.2: Use extension member (IsOpen)
            string estado = r.ToDomain().IsOpen() ? "ABIERTO" : "CERRADO";
            Console.WriteLine($"{r.Id}. {r.Nombre} ({r.Especialidad}) - {r.HorarioApertura} a {r.HorarioCierre} [{estado}]");
        }
    }

    static async Task RegistrarPedido(IPedidoUseCase useCase)
    {
        Console.Write("Usuario: ");
        string usuario = Console.ReadLine() ?? "anonimo";
        
        Console.Write("Costo Envío: ");
        if (!decimal.TryParse(Console.ReadLine(), out var costo)) costo = 0;

        var detalles = new List<DetallePedidoDto>();
        while (true)
        {
            Console.WriteLine("Agregar Platillo (0 para terminar):");
            Console.Write("ID Platillo: ");
            if (!int.TryParse(Console.ReadLine(), out var idPlatillo) || idPlatillo == 0) break;
            
            Console.Write("Cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out var cant)) cant = 1;
            
            Console.Write("Subtotal: ");
            if (!decimal.TryParse(Console.ReadLine(), out var sub)) sub = 0;

            detalles.Add(new DetallePedidoDto(idPlatillo, cant, sub));
        }

        if (detalles.Count > 0)
        {
            await useCase.RegisterAsync(new CreatePedidoDto(usuario, costo, detalles));
            Console.WriteLine("Pedido registrado con éxito.");
        }
    }

    static async Task ConsultarPedidos(IPedidoUseCase useCase)
    {
        Console.Write("Ingrese Usuario a consultar: ");
        string usuario = Console.ReadLine() ?? "";
        
        var pedidos = await useCase.GetByUsuarioAsync(usuario);
        Console.WriteLine($"\n--- Pedidos de {usuario} ---");
        foreach (var p in pedidos)
        {
            Console.WriteLine($"ID: {p.IdPedido} | Fecha: {p.FechaHora} | Envío: {p.CostoEnvio:C}");
            foreach (var d in p.Detalles)
            {
                Console.WriteLine($"  - Platillo {d.IdPlatillo} x {d.Cantidad} = {d.Subtotal:C}");
            }
        }
    }
}
