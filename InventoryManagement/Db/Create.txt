-- Criação da tabela de categorias
CREATE TABLE categories (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Criação da tabela de produtos
CREATE TABLE products (
    Id SERIAL PRIMARY KEY,
    CategoryId INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryId) REFERENCES categories(Id) ON DELETE SET NULL
);

-- Criação da tabela de clientes
CREATE TABLE customers (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Address TEXT,
    PhoneNumber VARCHAR(50),
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Criação da tabela de pedidos
CREATE TABLE orders (
    Id SERIAL PRIMARY KEY,
    CustomerId INT NOT NULL,
    OrderDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(50),
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerId) REFERENCES customers(Id) ON DELETE CASCADE
);

-- Criação da tabela de itens de pedidos
CREATE TABLE order_items (
    Id SERIAL PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (OrderId) REFERENCES orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES products(Id) ON DELETE CASCADE
);

-- Criação da tabela de movimentações de inventário
CREATE TABLE inventory_movements (
    Id SERIAL PRIMARY KEY,
    ProductId INT NOT NULL,
    QuantityChange INT NOT NULL,
    MovementDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MovementType  INT NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductId) REFERENCES products(Id) ON DELETE CASCADE
);