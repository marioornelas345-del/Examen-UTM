namespace FoodCampus.Domain.Extensions;

using FoodCampus.Domain.Entities;

public static class RestauranteExtensions
{
    // C# 14 Extension Property (Static member intent)
    public static bool IsOpen(this Restaurante r) => 
        TimeOnly.FromDateTime(DateTime.Now) >= r.HorarioApertura 
        && TimeOnly.FromDateTime(DateTime.Now) <= r.HorarioCierre;
}
