-- Cria o banco de dados
CREATE DATABASE Peoples;

-- Define qual banco de dados será utilizado
USE Peoples;

-- Cria a tabela Funcionarios
CREATE TABLE Funcionarios 
(
	IdFuncionario	INT IDENTITY PRIMARY KEY
	,Nome			VARCHAR(200) NOT NULL
	,Sobrenome		VARCHAR(255)
);
GO

CREATE TABLE TipoUsuario
(
	IdTipoUsuario	INT IDENTITY PRIMARY KEY
	,TipoUsuario		VARCHAR(200) NOT NULL
);
GO

CREATE TABLE Usuario
(
	IdUsuario	INT IDENTITY PRIMARY KEY
	,Nome	VARCHAR(200) NOT NULL
	IdTipoUsuario	INT FOREIGN KEY REFERENCES TipoUsuario (IdTipoUsuario)
);
GO

ALTER TABLE Usuario
ADD Senha VARCHAR (200)



-- Adiciona a coluna DataNascimento na tabela Funcionarios
ALTER TABLE Funcionarios
ADD DataNascimento DATE