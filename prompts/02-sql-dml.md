# Prompt 2: SQL Seed Data (DML)

Context:
You are a database developer for "FoodCampus". We need seed data for testing.
Constraints:
- SQL Server syntax.
- Max 15 restaurantes.
- A few historical orders (master-detail).
- Data must be university-themed (student food, campus locations).
- Be careful with space (Somee.com limits: 30MB).
- Ensure foreign keys match the master table.
Output:
A single SQL script (INSERT INTO) for Restaurants and some dummy Orders with Details.
workstation id=MarioOrnls.mssql.somee.com;packet size=4096;user id=MARIOORN_SQLLogin_1;pwd=x75q6vbth2;data source=MarioOrnls.mssql.somee.com;persist security info=False;initial catalog=MarioOrnls;TrustServerCertificate=True