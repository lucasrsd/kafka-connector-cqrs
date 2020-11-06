use lucasdb

select * From tb_proposta order by id desc 


insert into tb_proposta (nm_cliente, vl_proposta , ds_status, nm_vendedor, nm_produto, dt_proposta, dt_atualizacao)  
values ('AAAAAA', 1500, 'ANALISE', 'LUCAS', 'CELULAR', getdate(), getdate()) 

insert into tb_proposta (nm_cliente, vl_proposta , ds_status, nm_vendedor, nm_produto, dt_proposta, dt_atualizacao)  
values ('TESTE 123', 250, 'ANALISE', 'FULANO', 'CADEIRA', getdate(), getdate()) 

update tb_proposta set nm_cliente= 'TESTE NOVO',  nm_vendedor = 'FIZ UPDATE', dt_proposta = getdate() , dt_atualizacao = getdate()  



BEGIN
    WHILE 1 < 2
    BEGIN
      insert into tb_proposta (nm_cliente, vl_proposta , ds_status, nm_vendedor, nm_produto, dt_proposta, dt_atualizacao)  
		values ('CLIENTE SENDO INSERIDO', 5555, 'APROVADO', 'LUCAS', 'MOTO', getdate(), getdate()) 
    END;
END;

