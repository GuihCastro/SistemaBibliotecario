USE [master]
GO
 
-- Criação do banco de dados apontado para a pasta do projeto
CREATE DATABASE [BibliotecaDB] ON PRIMARY
(
    NAME = N'BibliotecaDB',
    FILENAME = N'$(ProjectDir)\App_Data\BibliotecaDB.mdf'
)
LOG ON
(
    NAME = N'BibliotecaDB_log',
    FILENAME = N'$(ProjectDir)\App_Data\BibliotecaDB_log.ldf'
)
GO

USE [BibliotecaDB]
GO

-- Criação das tabelas
CREATE TABLE Alunos (
    RA INT PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20) NOT NULL,
    DataNascimento DATE NOT NULL
);

CREATE TABLE Livros (
    Codigo INT PRIMARY KEY,
    Titulo NVARCHAR(100) NOT NULL,
    Autor NVARCHAR(100) NOT NULL,
    Categoria NVARCHAR(50),
    Editora NVARCHAR(50),
    Disponivel BIT NOT NULL DEFAULT 1
);

CREATE TABLE Emprestimos (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    RAAluno INT NOT NULL,
    CodigoLivro INT NOT NULL,
    DataRetirada DATETIME NOT NULL DEFAULT GETDATE(),
    DataEntrega DATETIME NOT NULL DEFAULT DATEADD(day, 7, GETDATE()),
    Devolvido BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (RAAluno) REFERENCES Alunos(RA),
    FOREIGN KEY (CodigoLivro) REFERENCES Livros(Codigo)
);