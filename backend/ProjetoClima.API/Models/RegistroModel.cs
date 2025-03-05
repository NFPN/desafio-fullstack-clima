using System.ComponentModel.DataAnnotations;

namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa o modelo de dados para o registro de um usuário
    /// </summary>
    public class RegistroModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = default!;
    }
}
