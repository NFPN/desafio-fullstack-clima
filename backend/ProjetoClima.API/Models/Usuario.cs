using Microsoft.AspNetCore.Identity;

namespace ProjetoClima.API.Models
{
    /// <summary>
    /// Representa um usuário do sistema, que pode favoritar cidades para monitorar o clima delas
    /// Deriva de IdentityUser para aproveitar a autenticação e autorização do ASP.NET Core Identity
    /// </summary>
    public class Usuario : IdentityUser { }
}
