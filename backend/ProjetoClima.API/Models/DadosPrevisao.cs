namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa o objeto principal da previsão do tempo da OpenWeather API
    /// </summary>
    public class DadosPrevisao
    {
        public List<PrevisaoItem> List { get; set; } = default!;
        public Cidade City { get; set; } = default!;
    }

    /// <summary>
    /// Representa os dados da previsão do tempo da OpenWeather API
    /// </summary>
    public class PrevisaoItem
    {
        /// <summary>
        ///   Timestamp unix, datetime
        /// </summary>
        public long Dt { get; set; }
        public DadosPrincipais Main { get; set; } = default!;
        public Clima[] Weather { get; set; } = default!;
    }

    public class Cidade
    {
        public string Name { get; set; } = default!;
    }
}
