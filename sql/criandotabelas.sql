CREATE DATABASE Cottoncandy;
USE Cottoncandy;

CREATE TABLE dbo.Genero (
   Id int IDENTITY(1,1) NOT NULL,
   Descricao varchar(50) NOT NULL,
   CONSTRAINT PK_Genero_Id PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE dbo.Usuario (
	Id int IDENTITY(1,1) NOT NULL,
	GeneroId int NOT NULL,
	Nome varchar(250) NOT NULL,
	Email varchar(100) NOT NULL,
	Senha varchar(200) NOT NULL,
	DataNascimento DateTime NOT NULL,
	FotoPerfil varchar(max) NOT NULL,
	Cargo varchar(200) NOT NULL,
	Cidade varchar(200) NOT NULL,
	CONSTRAINT PK_Usuario_Id PRIMARY KEY CLUSTERED (Id)
)

ALTER TABLE dbo.Usuario
   ADD CONSTRAINT FK_Usuario_Genero FOREIGN KEY (GeneroId)
      REFERENCES dbo.Genero (Id)

CREATE TABLE dbo.Postagem(
   Id int IDENTITY(1,1) NOT NULL,
   Texto varchar(max) NOT NULL,
   DataPostagem DateTime NOT NULL,
   Foto varchar(max), 
   UsuarioId int NOT NULL,   
   CONSTRAINT PK_Postagem_Id PRIMARY KEY CLUSTERED (Id) )

ALTER TABLE dbo.Postagem
   ADD CONSTRAINT FK_Postagem_Usuario FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)



CREATE TABLE dbo.Curtidas(
   Id int IDENTITY(1,1) NOT NULL, 
   Tipo varchar(100) NOT NULL, 
   PostagemId int NOT NULL, 
   UsuarioId int NOT NULL, 

  CONSTRAINT PK_Curtidas_Id PRIMARY KEY CLUSTERED (Id)
)

ALTER TABLE dbo.Curtidas
   ADD CONSTRAINT FK_Curtidas_Usuario FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)
	  
ALTER TABLE dbo.Curtidas
   ADD CONSTRAINT FK_Curtidas_Postagem FOREIGN KEY (PostagemId)
      REFERENCES dbo.Postagem (Id)



	