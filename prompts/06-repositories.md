# Prompt 6: Repository Interfaces and Implementations

Context:
Implement the Repository pattern for "FoodCampus".
Constraints:
- Application layer: Interface definitions (IRestauranteRepository, IPedidoRepository).
- Infrastructure layer: Implementations using Dapper and SQL Server.
- Use dependency injection for SqlConnection.
- Methods: AddRestaurante, GetAllRestaurantes, AddPedido (master-detail), GetPedidosByUser.
- Handle master-detail persistence within a transaction.
Output:
Repository interfaces and their concrete Dapper implementations.
