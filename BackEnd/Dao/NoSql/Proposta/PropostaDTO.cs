using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.WebApi.Dao.NoSql.Proposta
{
    public class PropostaDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int IdProposta { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
        public string Vendedor { get; set; }
        public string Produto { get; set; }
        public DateTime? DtProposta { get; set; }
        public DateTime? DtAtualizacao { get; set; }
    }
}
