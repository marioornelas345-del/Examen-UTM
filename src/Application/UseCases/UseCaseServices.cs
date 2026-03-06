using FoodCampus.Application.Persistence.Repositories;
using FoodCampus.Domain.Entities;
using FoodCampus.Application.DTOs;

namespace FoodCampus.Application.UseCases;

public class RestauranteService : IRestauranteUseCase
{
    private readonly IRestauranteRepository _repository;

    public RestauranteService(IRestauranteRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterAsync(CreateRestauranteDto dto)
    {
        var entity = new Restaurante
        {
            Nombre = dto.Nombre,
            Especialidad = dto.Especialidad,
            HorarioApertura = dto.HorarioApertura,
            HorarioCierre = dto.HorarioCierre
        };
        await _repository.AddAsync(entity);
    }

    public async Task<IEnumerable<RestauranteDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new RestauranteDto(e.Id, e.Nombre, e.Especialidad, e.HorarioApertura, e.HorarioCierre));
    }
}

public class PedidoService : IPedidoUseCase
{
    private readonly IPedidoRepository _repository;

    public PedidoService(IPedidoRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterAsync(CreatePedidoDto dto)
    {
        var entity = new Pedido
        {
            IdUsuario = dto.IdUsuario,
            CostoEnvio = dto.CostoEnvio,
            Detalles = dto.Detalles.Select(d => new DetallePedido
            {
                IdPlatillo = d.IdPlatillo,
                Cantidad = d.Cantidad,
                Subtotal = d.Subtotal
            }).ToList()
        };
        await _repository.AddAsync(entity);
    }

    public async Task<IEnumerable<PedidoDto>> GetByUsuarioAsync(string idUsuario)
    {
        var entities = await _repository.GetByUsuarioAsync(idUsuario);
        return entities.Select(e => new PedidoDto(
            e.IdPedido,
            e.IdUsuario,
            e.FechaHora,
            e.CostoEnvio,
            e.Detalles.Select(d => new DetallePedidoDto(d.IdPlatillo, d.Cantidad, d.Subtotal)).ToList()
        ));
    }
}

public class PlatilloService : IPlatilloUseCase
{
    private readonly IPlatilloRepository _repository;

    public PlatilloService(IPlatilloRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterAsync(CreatePlatilloDto dto)
    {
        var entity = new Platillo
        {
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            IdRestaurante = dto.IdRestaurante
        };
        await _repository.AddAsync(entity);
    }

    public async Task<IEnumerable<PlatilloDto>> GetByRestauranteAsync(int idRestaurante)
    {
        var entities = await _repository.GetByRestauranteAsync(idRestaurante);
        return entities.Select(e => new PlatilloDto(e.Id, e.Nombre, e.Precio, e.IdRestaurante));
    }
}
