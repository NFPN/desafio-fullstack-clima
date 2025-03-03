namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa a cidade favorita de um usuário, para fazer o monitoramento do clima
    /// </summary>
    public class CidadeFavorita
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string IdUsuario { get; set; }
    }
}
