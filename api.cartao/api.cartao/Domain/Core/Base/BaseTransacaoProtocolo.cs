namespace Domain.Core.Base
{
    public abstract class BaseTransacaoProtocolo
    {
        internal string _protocolo;

        public string Protocolo { get => _protocolo; }

        public string setTransacaoProtocolo(string id)
        {
            return generateProtocol(id);
        }


        private string generateProtocol(string id)
        {
            return $"{id}-{DateTime.Now.ToString("yyyymmdd")}{new Random().Next(1000, 9999)}";
        }

    }
}
