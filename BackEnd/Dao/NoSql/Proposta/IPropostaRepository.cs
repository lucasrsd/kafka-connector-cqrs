using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BackEnd.WebApi.Dao.NoSql.Proposta
{
    public interface IPropostaRepository
    {
        PropostaDTO InserirOuAtualizar(PropostaDTO proposta);
        PropostaDTO ObterPorProposta(int idProposta);
        List<PropostaDTO> ObterPorCliente(string cliente);
        List<PropostaDTO> ObterPorVendedor(string vendedor);
        List<PropostaDTO> ObterTodas();
    }
}
