-- Create the shoes table
 DROP TABLE IF EXISTS shoes;
CREATE TABLE Shoes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Brand VARCHAR(50) NOT NULL,
    Color VARCHAR(30) NOT NULL,
    Size INT NOT NULL,
    Stock INT NOT NULL
);

-- Insert 50 entries into the Shoes table
INSERT INTO Shoes (Brand, Color, Size, Stock) VALUES
('Nike', 'Red', 42, 10),
('Adidas', 'Blue', 40, 15),
('Puma', 'Green', 41, 5),
('Reebok', 'Black', 43, 8),
('New Balance', 'White', 39, 12),
('Asics', 'Yellow', 38, 7),
('Converse', 'Purple', 36, 20),
('Vans', 'Orange', 37, 30),
('Skechers', 'Pink', 41, 11),
('Under Armour', 'Gray', 44, 9),
('Saucony', 'Cyan', 45, 4),
('Brooks', 'Magenta', 39, 6),
('Hoka', 'Teal', 40, 5),
('Mizuno', 'Brown', 42, 3),
('On Cloud', 'Navy', 43, 2),
('K-Swiss', 'Olive', 38, 25),
('Fila', 'Turquoise', 41, 19),
('Merrell', 'Lime', 36, 14),
('Columbia', 'Crimson', 40, 16),
('Toms', 'Coral', 37, 12),
('Dr. Martens', 'Maroon', 45, 8),
('Clarks', 'Sand', 39, 13),
('ECCO', 'Slate', 44, 10),
('Palladium', 'Ivory', 38, 6),
('Native', 'Teal', 41, 22),
('Blundstone', 'Burgundy', 42, 11),
('Lowa', 'Mustard', 40, 5),
('Salomon', 'Forest Green', 43, 9),
('La Sportiva', 'Sky Blue', 36, 15),
('Altra', 'Dusty Rose', 44, 3),
('OluKai', 'Desert Tan', 39, 20),
('Havaianas', 'Seafoam', 37, 30),
('Crocs', 'Jade', 42, 18),
('Rothy’s', 'Eggplant', 38, 12),
('Allbirds', 'Peach', 45, 14),
('Vionic', 'Pewter', 41, 8),
('Sorel', 'Copper', 40, 7),
('Hiking Boots', 'Chestnut', 39, 10),
('Snow Boots', 'Silver', 44, 5),
('Running Shoes', 'Lavender', 36, 25),
('Basketball Shoes', 'Mint', 43, 2),
('Soccer Cleats', 'Charcoal', 42, 3),
('Tennis Shoes', 'Coral', 40, 4),
('Sandals', 'Light Pink', 39, 15),
('Flip Flops', 'Sky', 41, 9),
('Ankle Boots', 'Dark Gray', 36, 11),
('Loafers', 'Ivory', 44, 8),
('Dress Shoes', 'Burgundy', 42, 13),
('Work Boots', 'Army Green', 45, 10),
('Casual Shoes', 'Cream', 38, 20),
('Sneakers', 'Bright Red', 39, 22),
('Boots', 'Classic Black', 41, 12);

-- Optional: Select all entries to verify
SELECT * FROM Shoes;
