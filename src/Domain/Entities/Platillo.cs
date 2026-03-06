namespace FoodCampus.Domain.Entities;

public class Platillo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int IdRestaurante { get; set; }
}
