using Amazon.Auth.AccessControlPolicy;
using Domain.Core.Enums;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using static MongoDB.Libmongocrypt.CryptContext;

namespace Domain.Core.Base
{
    public record BaseReturn
    {
        public EnumReturnStatus? status { get; private set; }
        public string message { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? stack { get; private set; }

        public BaseReturn()
        {
            
        }

        public BaseReturn(EnumReturnStatus status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public BaseReturn Sucess(object retorno)
        {
            this.status = EnumReturnStatus.SUCESSO;
            this.message = JsonSerializer.Serialize(retorno);

            return this;
        }

        public BaseReturn SystemException(string message, string stack)
        {
            this.status = EnumReturnStatus.SISTEMA;
            this.message = message;
            this.stack = stack;

            return this;
           
        }

        public BaseReturn SystemException(Exception ex)
        {
            this.status = EnumReturnStatus.SISTEMA;
            this.message = ex.Message;
            this.stack = ex.StackTrace;

            return this;

        }

        public BaseReturn BussinesException(string message)
        {
            this.status = EnumReturnStatus.NEGOCIO;
            this.message = message;

            return this;
        }

        ~BaseReturn()
        {
            this.stack = null;
            this.status = null;
            this.message = null;
        }
    }
}
