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
    //[Authorize(Roles = "Produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoservice) 
        {
            _produtoService = produtoservice;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> GetAll() 
        {
            var produtos = await _produtoService.GetAll();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> Get(long id)
        {
           var produto = await _produtoService.GetId(id);
           return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Produto model)
        {
            var produto = await _produtoService.Add(model);

            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Produto model, long id)
        {
            var produto = await _produtoService.Update(model, id);

            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var produto = await _produtoService.Delete(id);
            return Ok(produto);
        }
    }
}
