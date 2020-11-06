use lucasdb

EXEC sys.sp_cdc_enable_db

select * From sys.databases where name = 'lucasdb'

exec msdb.dbo.rds_cdc_enable_db 'lucasdb' 

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name   = N'tb_proposta',
@role_name     = NULL,
@supports_net_changes = 1
GO 

SELECT * FROM [cdc].[dbo_tb_proposta_CT] order by dt_proposta desc 
