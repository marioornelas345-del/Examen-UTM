# Prompt 1: SQL Table Creation (DDL)

Context:
You are a SQL database architect. We are building "FoodCampus", a delivery system for a university. The database will be hosted on Somee.com (free plan), which has a 30MB limit for data and logs. We need a script that creates three main tables: Restaurante, Pedido, and DetallePedido.

Constraints:
- Use SQL Server syntax workstation id=MarioOrnls.mssql.somee.com;packet size=4096;user id=MARIOORN_SQLLogin_1;pwd=x75q6vbth2;data source=MarioOrnls.mssql.somee.com;persist security info=False;initial catalog=MarioOrnls;TrustServerCertificate=True
- Restaurante: Id (PK), Nombre (NVARCHAR(100), NOT NULL), Especialidad (NVARCHAR(100)), HorarioApertura (TIME), HorarioCierre (TIME).
- Pedido: IdPedido (PK), IdUsuario (NVARCHAR(50)), FechaHora (DATETIME), CostoEnvio (DECIMAL(18,2), check for >= 0).
- DetallePedido: IdDetalle (PK), IdPedido (FK), IdPlatillo (INT), Cantidad (INT, check for > 0), Subtotal (DECIMAL(18,2)).
- Ensure primary keys and foreign keys are correctly defined.
- Use efficient data types to save space.

Output:
A single SQL script for creating these tables.
