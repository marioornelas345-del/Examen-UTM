-- 01-sql-ddl.sql
-- Database creation script for FoodCampus
-- Optimized for Somee.com free plan (30MB limit)

CREATE TABLE Restaurante (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Especialidad NVARCHAR(100),
    HorarioApertura TIME,
    HorarioCierre TIME
);

CREATE TABLE Platillo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    IdRestaurante INT NOT NULL,
    CONSTRAINT FK_Platillo_Restaurante FOREIGN KEY (IdRestaurante) REFERENCES Restaurante(Id) ON DELETE CASCADE
);

CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario NVARCHAR(50) NOT NULL,
    FechaHora DATETIME NOT NULL DEFAULT GETDATE(),
    CostoEnvio DECIMAL(18,2) NOT NULL CHECK (CostoEnvio >= 0)
);

CREATE TABLE DetallePedido (
    IdDetalle INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT NOT NULL,
    IdPlatillo INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    Subtotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_DetallePedido_Pedido FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido) ON DELETE CASCADE
);
