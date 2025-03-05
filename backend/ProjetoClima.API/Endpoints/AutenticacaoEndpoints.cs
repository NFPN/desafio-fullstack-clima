using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoClima.API.Extensions;
using ProjetoClima.API.Models;

namespace ProjetoClima.API.Endpoints
{
    public static class AutenticacaoEndpoints
    {
        public static void MapAutenticacaoEndpoints(this IEndpointRouteBuilder app)
        {
            var nomeLoginEndpoint = "Login";
            var nomeRegistroEndpoint = "Registro";

            app.MapPost("/auth/registro", async (
                [FromServices] UserManager<Usuario> userManager,
                [FromBody] RegistroModel model) =>
            {
                var user = new Usuario { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return Results.Ok();

                return Results.BadRequest(result.Errors);
            }).WithName(nomeRegistroEndpoint);

            app.MapPost("/auth/login", async (
                [FromServices] SignInManager<Usuario> signInManager,
                [FromServices] IConfiguration configuration,
                [FromBody] LoginModel model) =>
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (!result.Succeeded)
                    return Results.Unauthorized();

                var usuario = await signInManager.UserManager.FindByEmailAsync(model.Email);
                var token = JwtExtensions.GerarJwtToken(usuario, configuration);

                return Results.Ok(new { Token = token });
            }).WithName(nomeLoginEndpoint);
        }
    }
}
