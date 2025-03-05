using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ProjetoClima.API.Models;
using System.Security.Claims;
using System.Text;

namespace ProjetoClima.API.Extensions
{
    public static class JwtExtensions
    {
        /// <summary>
        /// Método para adicionar a autenticação JWT ao projeto
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigurarJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
                };
            })
            .AddCookie("Identity.Application");
        }

        /// <summary>
        /// Método para gerar um token JWT para um usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GerarJwtToken(Usuario usuario, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    //new Claim("email_confirmed", usuario.EmailConfirmed.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var tokenHandler = new JsonWebTokenHandler();

            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
