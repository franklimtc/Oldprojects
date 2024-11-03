/*

Criação da role dos clietes

*/

DECLARE @roleCliente AS VARCHAR(MAX)
DECLARE @roleClienteDescription AS VARCHAR(MAX)
SET @roleCliente = 'Administradores'
SET @roleClienteDescription = 'Administradores da aplicação.'

INSERT INTO dbo.Roles(RoleId, ApplicationId, RoleName, Description)
(
	SELECT NEWID(), ApplicationId, @roleCliente, @roleClienteDescription FROM dbo.Applications
)


DECLARE @userid AS VARCHAR(MAX)
DECLARE @roleid AS VARCHAR(MAX)

SET @userid = (SELECT UserId from Users where UserName = 'Administrador')
SET @roleid = (SELECT RoleId from Roles where RoleName = 'Administradores')

INSERT INTO UsersinRoles(UserId, RoleId) VALUES(@userid, @roleid);

CREATE TABLE equipamentos
(
idEquipamento int identity(1,1) primary key
, uf varchar(2) not null
, municipio varchar(max)
, unidade varchar(max)
, endereco varchar(max)
, contato varchar(max)
, setor varchar(max)
, marca varchar(max)
, modelo varchar(max)
, serie varchar(max)
, contador int
, lote int
, nf  varchar(max)
)

CREATE TABLE UsersEquipamentos
(
	userId VARCHAR(MAX)
	, idEquipamento INT
)

CREATE TABLE requisicoes
(
	idrequisicao int identity(1,1) primary key
	, chave varchar(max) not null
	, codReq varchar(max)
	, serie varchar(max)
	, categoria varchar(max)
	, resumo varchar(max)
	, descricao varchar(max)
	, status varchar(max) default 'Aberto'
	, dtAbertura datetime default getdate()
	, dtFechamento datetime
	, dtModificacao datetime
	, abertorPor varchar(max) not null
	, modificadoPor varchar(max)
	, cliente varchar(max)
)

CREATE TABLE logsRequisicoes
(
	idlog int identity(1,1) primary key
	, codReq varchar(max) not null
	, usuario varchar(max) not null
	, tipo varchar(max) 
	, descricao varchar(max)
	, data datetime default getdate()
)

CREATE VIEW vwRequisicoes AS
(
	select idrequisicao, codReq, serie, categoria, resumo, descricao, 
	status, dtAbertura, dtModificacao, abertorPor, modificadoPor, cliente from requisicoes
	WHERE codReq is not null
)

CREATE VIEW vw_usersinroles AS
(
	select b.UserId, b.UserName, c.RoleId, c.RoleName  from usersinroles as a
	left join users as b on a.UserId=b.UserId
	left join roles as c on a.RoleId=c.RoleId
)

ALTER TABLE requisicoes ADD responsavel VARCHAR(MAX)


ALTER TABLE requisicoes ADD contador int, suprimento int

alter VIEW vwRequisicoes AS
(
	select idrequisicao, codReq, serie, categoria, resumo, descricao, 
	status, dtAbertura, dtModificacao, abertorPor, modificadoPor, cliente, contador, suprimento from requisicoes
	WHERE codReq is not null
)