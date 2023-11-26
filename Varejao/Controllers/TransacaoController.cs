using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Varejao.Models;
using Varejao.Repositorios.Interface;
using Varejao.Services;

namespace Varejao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoservice) 
        {
            _transacaoService = transacaoservice;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransacaoModel>>> GetAll()
        {
            var produtos = await _transacaoService.GetAll();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransacaoModel>> Get(long id)
        {
            var produto = await _transacaoService.GetId(id);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Transacao model)
        {
            var venda = await _transacaoService.Add(model);

            return Ok(venda);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var venda = await _transacaoService.Delete(id);
            return Ok(venda);
        }
    }
}
