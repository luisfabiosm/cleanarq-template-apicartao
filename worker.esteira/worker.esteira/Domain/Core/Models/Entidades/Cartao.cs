using Domain.Core.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Core.Models.Entidades
{

    public record Cartao
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; internal set; }
        public string NumeroCartao { get; set; }
        public string CVV { get; set; }
        public int DiaVencimento { get; set; }
        public EnumBandeiraCartao Bandeira { get; set; }
        public EnumTipoCartao TipoCartao { get; set; }
        public EnumStatusCartao StatusCartao { get; set; }

        public decimal Limite { get; set; }
        public Conta DadosConta { get; set; }

       
    }
}
