-- Criação da base de dados Sistema
CREATE DATABASE "Sistema"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-- Criação da tabela Fabricante
CREATE TABLE "Fabricante" (
    "IdFabricante" SERIAL PRIMARY KEY,    -- Chave primária autoincrementável para identificar unicamente cada fabricante
    "Nome" VARCHAR(255) NOT NULL,         -- Nome do fabricante com um máximo de 255 caracteres
    "Endereco" VARCHAR(255),              -- Endereço do fabricante, opcional
    "Telefone" VARCHAR(50)                -- Telefone do fabricante, opcional
);

-- Criação da tabela Produto com a referência para Fabricante
CREATE TABLE "Produto" (
    "IdProduto" SERIAL PRIMARY KEY,       -- Define IdProduto como uma chave primária autoincrementável
    "Nome" VARCHAR(255) NOT NULL,         -- Nome do produto com um máximo de 255 caracteres
    "IdFabricante" INTEGER,               -- Chave estrangeira que referencia Fabricante
    "Preco" DECIMAL(10, 2),               -- Preço do produto com duas casas decimais
    "Quantidade" INTEGER,                 -- Quantidade em estoque
    CONSTRAINT fk_fabricante
        FOREIGN KEY ("IdFabricante") 
        REFERENCES "Fabricante" ("IdFabricante") ON DELETE SET NULL
);


