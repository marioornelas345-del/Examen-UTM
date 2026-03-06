namespace FoodCampus.Application.DTOs;

public record RestauranteDto(int Id, string Nombre, string? Especialidad, TimeOnly HorarioApertura, TimeOnly HorarioCierre)
{
    public FoodCampus.Domain.Entities.Restaurante ToDomain() => new()
    {
        Id = Id,
        Nombre = Nombre,
        Especialidad = Especialidad,
        HorarioApertura = HorarioApertura,
        HorarioCierre = HorarioCierre
    };
}
public record CreateRestauranteDto(string Nombre, string? Especialidad, TimeOnly HorarioApertura, TimeOnly HorarioCierre);

public record PlatilloDto(int Id, string Nombre, decimal Precio, int IdRestaurante);
public record CreatePlatilloDto(string Nombre, decimal Precio, int IdRestaurante);

public record DetallePedidoDto(int IdPlatillo, int Cantidad, decimal Subtotal);
public record CreatePedidoDto(string IdUsuario, decimal CostoEnvio, List<DetallePedidoDto> Detalles);
public record PedidoDto(int IdPedido, string IdUsuario, DateTime FechaHora, decimal CostoEnvio, List<DetallePedidoDto> Detalles);
