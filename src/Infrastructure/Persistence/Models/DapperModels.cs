namespace FoodCampus.Infrastructure.Persistence.Models;

public class RestauranteModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
    public TimeSpan HorarioApertura { get; set; }
    public TimeSpan HorarioCierre { get; set; }
}

public class PlatilloModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int IdRestaurante { get; set; }
}

public class PedidoModel
{
    public int IdPedido { get; set; }
    public string IdUsuario { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public decimal CostoEnvio { get; set; }
}

public class DetallePedidoModel
{
    public int IdDetalle { get; set; }
    public int IdPedido { get; set; }
    public int IdPlatillo { get; set; }
    public int Cantidad { get; set; }
    public decimal Subtotal { get; set; }
}
