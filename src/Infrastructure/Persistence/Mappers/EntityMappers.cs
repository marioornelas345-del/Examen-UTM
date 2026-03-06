using FoodCampus.Domain.Entities;
using FoodCampus.Infrastructure.Persistence.Models;

namespace FoodCampus.Infrastructure.Persistence.Mappers;

public static class EntityMappers
{
    public static Restaurante ToDomain(this RestauranteModel model) => new()
    {
        Id = model.Id,
        Nombre = model.Nombre,
        Especialidad = model.Especialidad,
        HorarioApertura = TimeOnly.FromTimeSpan(model.HorarioApertura),
        HorarioCierre = TimeOnly.FromTimeSpan(model.HorarioCierre)
    };

    public static RestauranteModel ToModel(this Restaurante entity) => new()
    {
        Id = entity.Id,
        Nombre = entity.Nombre,
        Especialidad = entity.Especialidad,
        HorarioApertura = entity.HorarioApertura.ToTimeSpan(),
        HorarioCierre = entity.HorarioCierre.ToTimeSpan()
    };

    public static Platillo ToDomain(this PlatilloModel model) => new()
    {
        Id = model.Id,
        Nombre = model.Nombre,
        Precio = model.Precio,
        IdRestaurante = model.IdRestaurante
    };

    public static PlatilloModel ToModel(this Platillo entity) => new()
    {
        Id = entity.Id,
        Nombre = entity.Nombre,
        Precio = entity.Precio,
        IdRestaurante = entity.IdRestaurante
    };

    public static Pedido ToDomain(this PedidoModel model) => new()
    {
        IdPedido = model.IdPedido,
        IdUsuario = model.IdUsuario,
        FechaHora = model.FechaHora,
        CostoEnvio = model.CostoEnvio
    };

    public static PedidoModel ToModel(this Pedido entity) => new()
    {
        IdPedido = entity.IdPedido,
        IdUsuario = entity.IdUsuario,
        FechaHora = entity.FechaHora,
        CostoEnvio = entity.CostoEnvio
    };

    public static DetallePedido ToDomain(this DetallePedidoModel model) => new()
    {
        IdDetalle = model.IdDetalle,
        IdPedido = model.IdPedido,
        IdPlatillo = model.IdPlatillo,
        Cantidad = model.Cantidad,
        Subtotal = model.Subtotal
    };

    public static DetallePedidoModel ToModel(this DetallePedido entity) => new()
    {
        IdDetalle = entity.IdDetalle,
        IdPedido = entity.IdPedido,
        IdPlatillo = entity.IdPlatillo,
        Cantidad = entity.Cantidad,
        Subtotal = entity.Subtotal
    };
}
