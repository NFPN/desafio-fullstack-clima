namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa o modelo de dados de Clima da OpenWeather API
    /// </summary>
    public class DadosClima
    {
        /// <summary>
        /// Nome da cidade
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Dados principais do clima da cidade
        /// </summary>
        public DadosPrincipais Main { get; set; } = default!;

        /// <summary>
        /// Dados da descrição e ícone do clima da cidade
        /// </summary>
        public Clima[] Weather { get; set; } = default!;
    }

    /// <summary>
    /// Representa os dados principais do clima de uma cidade
    /// </summary>
    public class DadosPrincipais
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
    }

    /// <summary>
    /// Representa a descrição e o ícone do clima de uma cidade
    /// </summary>
    public class Clima
    {
        public string Description { get; set; } = default!;
        public string Icon { get; set; } = default!;
    }
}
