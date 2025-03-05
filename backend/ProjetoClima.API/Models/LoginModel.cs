using System.ComponentModel.DataAnnotations;

namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa o modelo de dados para o login de um usuário
    /// </summary>
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
