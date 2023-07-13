namespace Domain.Core.Models
{
    public record Titular
    {
        public string id { get; internal set; }
        public string CPF { get; set; }
        public string Nome { get; set; } = "";
        public DateTime DataNascimwento { get; set; }


        public int getIdade()
        {
            return DataNascimwento.Year - DateTime.Now.Year;
        }

        public string getNomeImpressao()
        {
            string[] partesNome = Nome.Split(' ');
            string primeiroNome = partesNome[0];
            string ultimoNome = partesNome[partesNome.Length - 1];

            if (partesNome.Length > 2)
            {
                string nomeMeio = partesNome[1];
                string inicialNomeMeio = nomeMeio.Substring(0, 1) + ".";
                primeiroNome += " " + inicialNomeMeio;
            }

            return $"{primeiroNome} {ultimoNome}";
        }
    }
}
