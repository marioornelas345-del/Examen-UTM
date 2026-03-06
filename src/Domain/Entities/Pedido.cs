namespace FoodCampus.Domain.Entities;

public class Pedido
{
    public int IdPedido { get; set; }
    public string IdUsuario { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; } = DateTime.Now;

    public decimal CostoEnvio 
    { 
        get; 
        set 
        {
            if (value < 0) throw new ArgumentException("El costo de envío no puede ser negativo.");
            field = value;
        } 
    }

    public List<DetallePedido> Detalles { get; set; } = new();
}

public class DetallePedido
{
    public int IdDetalle { get; set; }
    public int IdPedido { get; set; }
    public int IdPlatillo { get; set; }

    public int Cantidad 
    { 
        get; 
        set 
        {
            if (value <= 0) throw new ArgumentException("La cantidad debe ser mayor a 0.");
            field = value;
        } 
    }

    public decimal Subtotal { get; set; }
}
