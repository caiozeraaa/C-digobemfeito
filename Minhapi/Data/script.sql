-- 1. Criação do Banco de Dados
CREATE DATABASE if NOT EXISTS Minhapi;
use Minhapi;

-- 2. Criação da Tabela de Produtos
CREATE TABLE IF NOT EXISTS Produtos (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL
    preco DECIMAL(10, 2) NOT NULL,
    estoque INT NOT NULL DEFAULT 0,
    ativo TINYINT(1) NOT NULL DEFAULT 1
);
-- 3 inserção de dados iniciais (Carga)

INSERT INTO Produtos (nome, preco, estoque, ativo) VALUES
('Notebook', 3500, 10, 1),
('Mouse', 15.49, 50, 1),
('Teclado', 7.99, 200, 1),
('Monitor', 12.00, 0, 0);

select * from Produtos;
