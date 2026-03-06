using FoodCampus.Domain.Entities;

namespace FoodCampus.Application.Persistence.Repositories;

public interface IRestauranteRepository
{
    Task AddAsync(Restaurante restaurante);
    Task<IEnumerable<Restaurante>> GetAllAsync();
}

public interface IPedidoRepository
{
    Task AddAsync(Pedido pedido);
    Task<IEnumerable<Pedido>> GetByUsuarioAsync(string idUsuario);
}

public interface IPlatilloRepository
{
    Task AddAsync(Platillo platillo);
    Task<IEnumerable<Platillo>> GetByRestauranteAsync(int idRestaurante);
}

// Dummy generic interface for C# 14 demonstration
public interface IRepository<T> { }
