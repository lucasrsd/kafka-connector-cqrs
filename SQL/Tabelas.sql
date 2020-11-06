use lucasdb

create table tb_proposta
(
id int identity primary key,
nm_cliente nvarchar(50),
vl_proposta float,
ds_status nvarchar(50),
nm_vendedor nvarchar(50),
nm_produto nvarchar(50),
dt_proposta datetime,
dt_atualizacao datetime2  not null 
)

