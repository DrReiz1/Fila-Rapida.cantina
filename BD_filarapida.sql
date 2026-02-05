CREATE DATABASE IF NOT EXISTS fila_rapida;
USE fila_rapida;

CREATE TABLE IF NOT EXISTS usuarios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(80) NOT NULL,
  login VARCHAR(40) NOT NULL UNIQUE,
  senha_hash VARCHAR(255) NOT NULL,
  perfil ENUM('ADMIN','ATENDENTE') NOT NULL DEFAULT 'ATENDENTE',
  ativo TINYINT(1) NOT NULL DEFAULT 1,
  criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS produtos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(80) NOT NULL,
  preco DECIMAL(10,2) NOT NULL,
  ativo TINYINT(1) NOT NULL DEFAULT 1,
  criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS pedidos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario_id INT NOT NULL,
  criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  total DECIMAL(10,2) NOT NULL DEFAULT 0,
  FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

CREATE TABLE IF NOT EXISTS pedido_itens (
  id INT AUTO_INCREMENT PRIMARY KEY,
  pedido_id INT NOT NULL,
  produto_id INT NOT NULL,
  qtd INT NOT NULL,
  preco_unit DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (pedido_id) REFERENCES pedidos(id),
  FOREIGN KEY (produto_id) REFERENCES produtos(id)
);
 
INSERT INTO usuarios (nome, login, senha_hash, perfil) VALUES 
('Administrador', 'admin', 'admin123', 'ADMIN'),
('Atendente 1', 'at1', '123', 'ATENDENTE')
ON DUPLICATE KEY UPDATE
nome = VALUES(nome),
perfil = VALUES(perfil);

INSERT INTO produtos (nome, preco)
VALUES 
('Coxinha', 7.50),
('Pão de queijo', 5.00),
('Suco', 6.00),
('Refrigerante', 6.50),
('Bolo', 8.00)
ON DUPLICATE KEY UPDATE
preco = VALUES(preco);

-- Produtos ativos
SELECT * FROM produtos WHERE ativo = 1;

-- Buscar por parte do nome
SELECT * FROM produtos WHERE nome LIKE '%co%';

-- Usuários por perfil
SELECT nome, perfil FROM usuarios;

ALTER TABLE produtos
ADD CONSTRAINT uq_produtos_nome UNIQUE (nome);


SELECT id, usuario_id, criado_em, total
FROM pedidos
ORDER BY criado_em DESC;


SELECT id, nome, perfil
FROM usuarios
WHERE login = 'admin'
  AND senha_hash = 'admin123'
  AND ativo = 1;

UPDATE usuarios
SET senha_hash = '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9'
WHERE login = 'admin';

SELECT login, senha_hash FROM usuarios WHERE login = 'admin';
SELECT login, senha_hash FROM usuarios WHERE login = 'at2';

USE fila_rapida;

ALTER TABLE produtos 
ADD quantidade INT NOT NULL DEFAULT 0;

ALTER TABLE produtos 
ADD estoque_min INT NOT NULL DEFAULT 5;

USE fila_rapida;

CREATE TABLE IF NOT EXISTS pedidos_reposicao (
    id INT AUTO_INCREMENT PRIMARY KEY,
    produto_id INT,
    quantidade INT,
    status VARCHAR(20) DEFAULT 'PENDENTE',
    criado_em DATETIME DEFAULT CURRENT_TIMESTAMP
);

SELECT * FROM produtos;

SELECT nome, preco, quantidade FROM produtos;

