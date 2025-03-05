using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa a cidade favorita de um usuário, para fazer o monitoramento do clima
    /// </summary>
    public class CidadeFavorita
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NomeCidade { get; set; } = default!;

        [ForeignKey("IdUsuario")]
        public string IdUsuario { get; set; } = default!;
    }
}
