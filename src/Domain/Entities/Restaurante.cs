namespace FoodCampus.Domain.Entities;

public class Restaurante
{
    public int Id { get; set; }
    
    public string Nombre 
    { 
        get; 
        set 
        {
            if (string.IsNullOrWhiteSpace(value)) 
                throw new ArgumentException("El nombre es obligatorio.");
            field = value;
        } 
    } = string.Empty;

    public string? Especialidad { get; set; }
    public TimeOnly HorarioApertura { get; set; }
    public TimeOnly HorarioCierre { get; set; }
}
