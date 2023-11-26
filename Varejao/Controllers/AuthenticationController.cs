using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Varejao.Data;
using Varejao.Models;
using Varejao.Services;
using Varejao_api.Enums;

namespace Varejao.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SistemaVarejaoDbContext _dbContext;

        public AuthenticationController(SistemaVarejaoDbContext sistemaVarejaoDbContext)
        {
            _dbContext = sistemaVarejaoDbContext;
        }

        /// <summary>
        /// Authenticate with a user credential and application access key
        /// </summary>
        /// <param name="model">User and password</param>
        /// <remarks>Contact us to obtain your access key</remarks>
        /// <response code="200">Access token</response>
        /// <response code="400">Invalid user/password<br />Invalid access key</response>
        /// <response code="401">Access denied for this IP address<br />Access denied for this access key</response>
        /// <response code="404">User not found</response>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginModel model)
        {
            var usuario = _dbContext.Usuario.SingleOrDefault(x => x.Login == model.User && x.Senha == model.Password);
            if (usuario == null)
                return NotFound("User not found");

            var usuarioLogado = new LoggedUserModel()
            {
                Id = usuario.Id,
                Apelido = usuario.Login,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Nivel = usuario.Cargo.ToString(),
            };

            if (usuarioLogado.Nivel == "ADMIN")
            {
                usuarioLogado.Roles.Add("Admin");
            }

            var token = TokenService.GenerateToken(usuarioLogado);

            return await Task.FromResult(new
            {
                //user = usuarioLogado,
                token = token
            });
        }

    }
}
