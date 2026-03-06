using FoodCampus.Application.DTOs;

namespace FoodCampus.Application.UseCases;

public interface IRestauranteUseCase
{
    Task RegisterAsync(CreateRestauranteDto dto);
    Task<IEnumerable<RestauranteDto>> GetAllAsync();
}

public interface IPedidoUseCase
{
    Task RegisterAsync(CreatePedidoDto dto);
    Task<IEnumerable<PedidoDto>> GetByUsuarioAsync(string idUsuario);
}

public interface IPlatilloUseCase
{
    Task RegisterAsync(CreatePlatilloDto dto);
    Task<IEnumerable<PlatilloDto>> GetByRestauranteAsync(int idRestaurante);
}
