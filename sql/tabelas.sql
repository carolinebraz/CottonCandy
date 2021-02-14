--Criação do Database
CREATE DATABASE CottonCandy;
USE CottonCandy;

--Criação da tabela de gênero
CREATE TABLE dbo.Genero (
   Id int IDENTITY(1,1) NOT NULL,
   Descricao varchar(50) NOT NULL,
   CONSTRAINT PK_Genero_Id PRIMARY KEY CLUSTERED (Id)
)

--Criação da tabela de usuário
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

--Criação da tabela de postagem
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

--Criação da tabela de comentários
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

--Criação da tabela de curtidas
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

--Inserção de valores na tabela de gêneros
INSERT INTO Genero VALUES ('Feminino')
INSERT INTO Genero VALUES ('Masculino')
INSERT INTO Genero VALUES ('Personalizado')

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

SELECT * FROM Usuario
SELECT * FROM Genero
SELECT * FROM Postagem
SELECT * FROM Curtidas
SELECT * FROM Comentario
