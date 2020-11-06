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
    public interface IPropostaDao
    {
         void CadastrarNovaProposta(PropostaRequest proposta);
    }
}
