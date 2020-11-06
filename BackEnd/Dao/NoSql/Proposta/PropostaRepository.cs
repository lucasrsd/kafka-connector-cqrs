using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq;

namespace BackEnd.WebApi.Dao.NoSql.Proposta
{
    public class PropostaRepository : IPropostaRepository
    {
        private readonly IMongoCollection<PropostaDTO> propostasCollection;

        public PropostaRepository()
        {

            var client = new MongoClient("mongodb+srv://USUARIO:SENHA@clusterXXXX.mongodb.net/MeuMongoDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("MeuMongoDB");
            propostasCollection = database.GetCollection<PropostaDTO>("Propostas");
        }

        private PropostaDTO Criar(PropostaDTO proposta)
        {
            propostasCollection.InsertOne(proposta);
            return proposta;
        }

        private PropostaDTO Atualizar(PropostaDTO proposta)
        {
            propostasCollection.ReplaceOne(x => x.Id == proposta.Id, proposta);
            return proposta;
        }

        public PropostaDTO ObterPorProposta(int idProposta)
        {
            return propostasCollection.Find(x => x.IdProposta == idProposta).FirstOrDefault();
        }

        public List<PropostaDTO> ObterPorCliente(string cliente)
        {
            return propostasCollection.Find(x => x.Cliente == cliente).ToList();
        }

        public List<PropostaDTO> ObterPorVendedor(string vendedor)
        {
            return propostasCollection.Find(x => x.Vendedor == vendedor).ToList();
        }

        public List<PropostaDTO> ObterTodas()
        {
            return propostasCollection.Find(new BsonDocument()).ToList().OrderByDescending(X => X.IdProposta).Take(100).ToList();
        }

        public PropostaDTO InserirOuAtualizar(PropostaDTO proposta)
        {
            var propostaMongo = ObterPorProposta(proposta.IdProposta);

            if (propostaMongo != null)
            {
                proposta.Id = propostaMongo.Id;
                Atualizar(proposta);
            }
            else
                Criar(proposta);

            return proposta;
        }
    }
}
