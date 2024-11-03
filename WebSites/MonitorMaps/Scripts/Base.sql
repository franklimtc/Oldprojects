--create database Maps;

USE maps;

create table markers
(
	idmarker int identity(1,1) primary key
	, descricao varchar(max)
	, nomeSimples varchar(max)
	, lat varchar(max)
	, lng varchar(max)
	, data datetime default getdate()
)


insert into markers(descricao, nomeSimples, lat, lng) VALUES('CAPGV','CAPGV','-3.8089611','-38.5357721')
insert into markers(descricao, nomeSimples, lat, lng) VALUES('Agência Montese','AG_Montese','-3.7787388','-38.5680174')
insert into markers(descricao, nomeSimples, lat, lng) VALUES('Agência Centro','AG_Centro','-3.7516077','-38.5576999')

select descricao, nomeSimples, lat, lng from dbo.markers
