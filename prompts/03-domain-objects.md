# Prompt 3: Domain Objects (with C# 14 field keyword)

Context:
You are a senior .NET developer building "FoodCampus".
We need Domain entities: Restaurante, Pedido, DetallePedido.
Constraints:
- C# 14, .NET 10.
- Use the 'field' keyword for validation within properties (no private backing fields).
- Restaurante: Id, Nombre (required), Especialidad, HorarioApertura, HorarioCierre.
- Pedido: IdPedido, IdUsuario, FechaHora, CostoEnvio (>= 0).
- DetallePedido: IdDetalle, IdPedido, IdPlatillo, Cantidad (> 0), Subtotal.
- Domain must have zero external dependencies.
- Add an extension property (static extension member) to check if a Restaurante is open (IsOpen).
Output:
Three C# entity classes and a class for the extension property.
