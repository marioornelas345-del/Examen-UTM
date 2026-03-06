-- 02-sql-dml.sql
-- Seed data for FoodCampus
-- University-themed, space-efficient

-- 1. Restaurantes (Max 15)
INSERT INTO Restaurante (Nombre, Especialidad, HorarioApertura, HorarioCierre) VALUES 
('La Cafeta Campus', 'Desayunos y Café', '07:00:00', '18:00:00'),
('Pizza del Estudiante', 'Pizzas Rápidas', '11:00:00', '22:00:00'),
('Tacos Universitarios', 'Tacos y Burritos', '10:00:00', '21:00:00'),
('Ensaladas Veritas', 'Saludable', '09:00:00', '16:00:00'),
('Burger Hall', 'Hamburguesas', '12:00:00', '23:00:00'),
('Sushi Study', 'Sushi', '12:00:00', '21:00:00'),
('Dona Sabiduría', 'Postres', '08:00:00', '20:00:00'),
('Pasta Grado', 'Pastas', '11:00:00', '21:00:00'),
('Wok Académico', 'Comida China', '12:00:00', '22:00:00'),
('Comedor Central', 'Menú del Día', '12:00:00', '15:00:00');

-- 2. Platillos (Menus for ALL restaurants)
INSERT INTO Platillo (Nombre, Precio, IdRestaurante) VALUES 
('Combo Kiwi Express', 4.50, 1), -- La Cafeta Campus
('Bowl de Avena y Frutas', 5.00, 1),
('Pizza Pepperoni Uni', 8.50, 2), -- Pizza del Estudiante
('Calzone Académico', 7.00, 2),
('Taco de Pastor', 1.50, 3), -- Tacos Universitarios
('Burrito de Pollo', 4.50, 3),
('Ensalada César', 5.50, 4), -- Ensaladas Veritas
('Bowl Quinoa Power', 6.50, 4),
('Burger Mega Campus', 7.50, 5), -- Burger Hall
('Papas con Queso', 3.00, 5),
('Sushi Roll Filadelfia', 9.00, 6), -- Sushi Study
('Sashimi Salmón', 11.00, 6),
('Dona Glaseada', 1.25, 7), -- Dona Sabiduría
('Muffin de Arándanos', 2.00, 7),
('Fettuccine Alfredo', 8.50, 8), -- Pasta Grado
('Lasaña de Carne', 9.50, 8),
('Arroz Chaufa Estudiante', 6.00, 9), -- Wok Académico
('Pollo con Almendras', 7.50, 9),
('Menú Económico Res', 3.50, 10), -- Comedor Central
('Menú Económico Pollo', 3.50, 10);

-- 3. Pedidos Históricos
-- Pedido 1
INSERT INTO Pedido (IdUsuario, FechaHora, CostoEnvio) VALUES ('mario.estudiante', '2025-03-01 13:00:00', 1.50);
DECLARE @PedidoId1 INT = SCOPE_IDENTITY();
INSERT INTO DetallePedido (IdPedido, IdPlatillo, Cantidad, Subtotal) VALUES 
(@PedidoId1, 101, 2, 12.00), -- 2 Hamburguesas
(@PedidoId1, 202, 1, 1.50);  -- 1 Refresco

-- Pedido 2
INSERT INTO Pedido (IdUsuario, FechaHora, CostoEnvio) VALUES ('ana.investigadora', '2025-03-02 08:30:00', 0.50);
DECLARE @PedidoId2 INT = SCOPE_IDENTITY();
INSERT INTO DetallePedido (IdPedido, IdPlatillo, Cantidad, Subtotal) VALUES 
(@PedidoId2, 301, 1, 3.50), -- 1 Café
(@PedidoId2, 302, 1, 2.00);  -- 1 Muffin
