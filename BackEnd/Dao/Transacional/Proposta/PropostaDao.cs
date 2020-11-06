using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using BackEnd.WebApi.Models.Proposta;

namespace BackEnd.WebApi.Dao.Transacional.Proposta
{
    public class PropostaDao : IPropostaDao
    {
        private readonly string connectionString = @"Server=tcp:{SEU_SERVIDOR}.amazonaws.com,1433;Initial Catalog={SEU_DB};Persist Security Info=False;User ID={USUARIO};Password={SENHA};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=180;Pooling=false;";

        public void CadastrarNovaProposta(PropostaRequest proposta)
        {
            var query = @"insert into tb_proposta (nm_cliente, vl_proposta , ds_status, nm_vendedor, nm_produto, dt_proposta, dt_atualizacao)  
                        values (@nomeCliente, @valorProposta, @status, @vendedor, @produto, getdate(), getdate()) ";
            var objectParams = new { nomeCliente = proposta.Nome, valorProposta = proposta.Valor, status = "ANALISE", vendedor = proposta.Vendedor, produto = proposta.Produto };
            Executar(query, objectParams);
        }

        private void Executar(string query, object values)
        {
            using (var sqlInstance = new SqlConnection(this.connectionString))
            {
                sqlInstance.Query(query, values, commandTimeout: 180);
            }
        }
    }
}
