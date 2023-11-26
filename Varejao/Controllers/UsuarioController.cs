using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Varejao.Models;
using Varejao.Repositorios.Interface;

namespace Varejao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Usuario,Admin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) 
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UsuarioModel>>> GetAll() 
        {
            var usuarios = await _usuarioService.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProdutoModel>> Get(long id)
        {
           var usuario = await _usuarioService.GetId(id);
           return Ok(usuario);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Usuario model)
        {
            var usuario = await _usuarioService.Add(model);

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] Usuario model, long id)
        {
            var usuario = await _usuarioService.Update(model, id);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var produto = await _usuarioService.Delete(id);
            return Ok(produto);
        }

    }
}
