--Cria Database
CREATE DATABASE CottonCandy;
USE CottonCandy;

--Cria tabela de gênero
CREATE TABLE dbo.Genero (
   Id int IDENTITY(1,1) NOT NULL,
   Descricao varchar(50) NOT NULL,
   CONSTRAINT PK_Genero_Id PRIMARY KEY CLUSTERED (Id)
)

--Cria tabela de usuário
CREATE TABLE dbo.Usuario (
	Id int IDENTITY(1,1) NOT NULL,
	GeneroId int NOT NULL,
	Nome varchar(255) NOT NULL,
	Email varchar(100) NOT NULL,
	Senha varchar(255) NOT NULL,
	DataNascimento DateTime NOT NULL,
	FotoPerfil varchar(max) NOT NULL,
	Cargo varchar(50) NOT NULL,
	Cidade varchar(50) NOT NULL,
	CONSTRAINT PK_Usuario_Id PRIMARY KEY CLUSTERED (Id),

	CONSTRAINT FK_Usuario_Genero FOREIGN KEY (GeneroId)
		REFERENCES dbo.Genero (Id)
)

--Cria tabela de postagem
CREATE TABLE dbo.Postagem (
   Id int IDENTITY(1,1) NOT NULL,
   UsuarioId int NOT NULL,
   Texto varchar(300) NOT NULL,
   DataPostagem DateTime NOT NULL,
   FotoPost varchar(max)
   CONSTRAINT PK_Postagem_Id PRIMARY KEY CLUSTERED (Id),

   CONSTRAINT FK_Postagem_Usuario FOREIGN KEY (UsuarioId)
		REFERENCES dbo.Usuario (Id)
)

--Cria tabela de comentários
CREATE TABLE dbo.Comentario (
	Id int IDENTITY(1,1) NOT NULL,
	UsuarioId int NOT NULL,
	PostagemId int NOT NULL,
	Texto varchar(255) NOT NULL,
	DataComentario DateTime NOT NULL,
	CONSTRAINT PK_Comentario_Id PRIMARY KEY CLUSTERED (Id),

	CONSTRAINT FK_Comentario_Usuario FOREIGN KEY (UsuarioId)
		REFERENCES dbo.Usuario (Id),

	CONSTRAINT FK_Comentario_Postagem FOREIGN KEY (PostagemId)
		REFERENCES dbo.Postagem (Id)
)

--Cria tabela de curtidas
CREATE TABLE dbo.Curtidas (
	Id int IDENTITY(1,1) NOT NULL,
	UsuarioId int NOT NULL,
	PostagemId int NOT NULL,
	CONSTRAINT PK_Curtidas_Id PRIMARY KEY CLUSTERED (Id),

	CONSTRAINT FK_Curtidas_Usuario FOREIGN KEY (UsuarioId)
		REFERENCES dbo.Usuario (Id),

	CONSTRAINT FK_Curtidas_Postagem FOREIGN KEY (PostagemId)
		REFERENCES dbo.Postagem (Id)
)

--Cria tabela de amigos
CREATE TABLE dbo.Amigos (
	Id int IDENTITY(1,1) NOT NULL,
	IdSeguidor int NOT NULL,
	IdSeguido int NOT NULL,
	CONSTRAINT PK_Amigos_Id PRIMARY KEY CLUSTERED (Id),

	CONSTRAINT FK_Amigos_Usuario FOREIGN KEY (IdSeguidor)
		REFERENCES dbo.Usuario (Id)

	CONSTRAINT FK_Usuario_Amigos FOREIGN KEY (IdSeguido)
		REFERENCES dbo.Usuario (id)
)

--Insere valores na tabela de gênero
INSERT INTO Genero VALUES ('Feminino')
INSERT INTO Genero VALUES ('Masculino')
INSERT INTO Genero VALUES ('Personalizado')

--Atualiza tabela de gênero
UPDATE Genero
	SET Descricao = ('Neutro')
	WHERE Id = 3

--Altera colunas FotoPerfil, Cargo e Cidade na tabela de usuário
ALTER TABLE Usuario
	ALTER COLUMN FotoPerfil varchar(max) NULL
ALTER TABLE Usuario
	ALTER COLUMN Cargo varchar(50) NULL
ALTER TABLE Usuario
	ALTER COLUMN Cidade varchar(50) NULL

--Insere coluna FotoCapa na tabela de usuário
ALTER TABLE Usuario
	ADD FotoCapa varchar(max)
	
--Retorna registros correspondentes nas tabelas de usuário e gênero
SELECT
	u.Id,
	u.Nome,
	u.Email,
	u.Senha,
	g.Id as GeneroId,
	g.Descricao
FROM 
	Usuario u
INNER JOIN 
	Genero g ON g.Id = u.GeneroId
	
--Visualiza tabelas
SELECT * FROM Usuario
SELECT * FROM Genero
SELECT * FROM Postagem
SELECT * FROM Curtidas
SELECT * FROM Comentario
SELECT * FROM Amigos

--Apaga todas as linhas das tabelas
DELETE FROM Usuario
DELETE FROM Postagem
DELETE FROM Curtidas
DELETE FROM Comentario
DELETE FROM Amigos

--Reinicia contagem de IDENTITY (zera colunas Id)
DBCC CHECKIDENT(Usuario, RESEED, 0)
DBCC CHECKIDENT(Postagem, RESEED, 0)
DBCC CHECKIDENT(Curtidas, RESEED, 0)
DBCC CHECKIDENT(Comentario, RESEED, 0)
DBCC CHECKIDENT(Amigos, RESEED, 0)
