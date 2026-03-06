using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using FoodCampus.Application.Persistence.Repositories;
using FoodCampus.Domain.Entities;
using FoodCampus.Infrastructure.Persistence.Mappers;
using FoodCampus.Infrastructure.Persistence.Models;

namespace FoodCampus.Infrastructure.Persistence.Repositories;

public class DapperRestauranteRepository : IRestauranteRepository
{
    private readonly string _connectionString;

    public DapperRestauranteRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Restaurante restaurante)
    {
        var model = restaurante.ToModel();
        using var db = new SqlConnection(_connectionString);
        string sql = "INSERT INTO Restaurante (Nombre, Especialidad, HorarioApertura, HorarioCierre) VALUES (@Nombre, @Especialidad, @HorarioApertura, @HorarioCierre)";
        await db.ExecuteAsync(sql, model);
    }

    public async Task<IEnumerable<Restaurante>> GetAllAsync()
    {
        using var db = new SqlConnection(_connectionString);
        var models = await db.QueryAsync<RestauranteModel>("SELECT * FROM Restaurante");
        return models.Select(m => m.ToDomain());
    }
}

public class DapperPedidoRepository : IPedidoRepository
{
    private readonly string _connectionString;

    public DapperPedidoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Pedido pedido)
    {
        using var db = new SqlConnection(_connectionString);
        await db.OpenAsync();
        using var transaction = db.BeginTransaction();

        try
        {
            var pedidoModel = pedido.ToModel();
            string sqlPedido = "INSERT INTO Pedido (IdUsuario, FechaHora, CostoEnvio) VALUES (@IdUsuario, @FechaHora, @CostoEnvio); SELECT CAST(SCOPE_IDENTITY() as int)";
            int idPedido = await db.QuerySingleAsync<int>(sqlPedido, pedidoModel, transaction);

            foreach (var detalle in pedido.Detalles)
            {
                detalle.IdPedido = idPedido;
                var detalleModel = detalle.ToModel();
                string sqlDetalle = "INSERT INTO DetallePedido (IdPedido, IdPlatillo, Cantidad, Subtotal) VALUES (@IdPedido, @IdPlatillo, @Cantidad, @Subtotal)";
                await db.ExecuteAsync(sqlDetalle, detalleModel, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<IEnumerable<Pedido>> GetByUsuarioAsync(string idUsuario)
    {
        using var db = new SqlConnection(_connectionString);
        string sql = "SELECT * FROM Pedido WHERE IdUsuario = @IdUsuario";
        var pedidos = await db.QueryAsync<PedidoModel>(sql, new { IdUsuario = idUsuario });
        
        var result = new List<Pedido>();
        foreach (var pModel in pedidos)
        {
            var p = pModel.ToDomain();
            string sqlDetalle = "SELECT * FROM DetallePedido WHERE IdPedido = @IdPedido";
            var detalles = await db.QueryAsync<DetallePedidoModel>(sqlDetalle, new { IdPedido = p.IdPedido });
            p.Detalles = detalles.Select(d => d.ToDomain()).ToList();
            result.Add(p);
        }
        return result;
    }
}

public class DapperPlatilloRepository : IPlatilloRepository
{
    private readonly string _connectionString;

    public DapperPlatilloRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Platillo platillo)
    {
        var model = platillo.ToModel();
        using var db = new SqlConnection(_connectionString);
        string sql = "INSERT INTO Platillo (Nombre, Precio, IdRestaurante) VALUES (@Nombre, @Precio, @IdRestaurante)";
        await db.ExecuteAsync(sql, model);
    }

    public async Task<IEnumerable<Platillo>> GetByRestauranteAsync(int idRestaurante)
    {
        using var db = new SqlConnection(_connectionString);
        string sql = "SELECT * FROM Platillo WHERE IdRestaurante = @IdRestaurante";
        var models = await db.QueryAsync<PlatilloModel>(sql, new { IdRestaurante = idRestaurante });
        return models.Select(m => m.ToDomain());
    }
}
