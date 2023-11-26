using Microsoft.EntityFrameworkCore;
using System.Data;
using Varejao.Data;
using Varejao.Models;
using Varejao.Repositorios.Interface;
using System.Linq;
using Varejao_api.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Varejao.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly SistemaVarejaoDbContext _dbContext;

        public TransacaoService(SistemaVarejaoDbContext sistemaVarejaoDbContext) 
        { 
            _dbContext = sistemaVarejaoDbContext;
        }

        public async Task<Transacao> Get(long id)
        {
            var venda = await _dbContext.Transacao.FirstOrDefaultAsync(x => x.Id == id);

            if (venda == null)
                throw new Exception($"Registro de venda não existente");

            return venda;
        }

        public async Task<TransacaoModel> GetId(long id)
        {
            var venda = await _dbContext.Transacao.FirstOrDefaultAsync(x => x.Id == id);

            if (venda == null)
            {
                throw new Exception($"Venda não existente");
            }

            var produto = await _dbContext.Produto.FirstOrDefaultAsync(p => p.Id == venda.ProdutoId);

            var transacaoModel = new TransacaoModel(
                venda.Id,
                produto.Nome, 
                venda.Quantidade,
                venda.Categoria.ToString()
            );

            return transacaoModel;
        }

        public async Task<List<TransacaoModel>> GetAll()
        {
            var vendas = await _dbContext.Transacao.ToListAsync();
            var produtosIds = vendas.Select(v => v.ProdutoId).ToList();

            var produtos = await _dbContext.Produto.Where(p => produtosIds.Contains(p.Id)).ToListAsync();

            var transacoesModel = vendas.Select(v =>
            {
                var produto = produtos.FirstOrDefault(p => p.Id == v.ProdutoId);
                return new TransacaoModel(
                    v.Id,
                    produto != null ? produto.Nome : string.Empty,
                    v.Quantidade,
                    v.Categoria.ToString()
                );
            }).ToList();

            return transacoesModel;
        }

        public async Task<long> Add(Transacao transacao)
        {
            ValidarObrigatoriedade(transacao);

            var produto = await _dbContext.Produto.FirstOrDefaultAsync(p => p.Id == transacao.ProdutoId);

            transacao.Categoria = produto.Categoria;
            transacao.DataVenda = DateTime.Now;

            if (produto == null)
            {
                throw new Exception($"Produto não encontrado com o nome: {transacao.ProdutoId}");
            }

            if (produto.Quantidade < transacao.Quantidade)
            {
                throw new Exception("Quantidade insuficiente do produto em estoque.");
            }

            produto.Quantidade -= transacao.Quantidade;
            produto.Lucro += (produto.PrecoVenda - produto.PrecoCusto) * transacao.Quantidade; 

            await _dbContext.Transacao.AddAsync(transacao);
            _dbContext.Produto.Update(produto);

            await _dbContext.SaveChangesAsync();

            return transacao.Id;
        }


        public async Task<long> Delete(long id)
        {

            var vendaId = await Get(id);

            var produto = await _dbContext.Produto.FirstOrDefaultAsync(p => p.Id == vendaId.ProdutoId);

            produto.Quantidade += vendaId.Quantidade;
            produto.Lucro -= (produto.PrecoVenda - produto.PrecoCusto) * vendaId.Quantidade;

            if (vendaId == null)
                throw new Exception($"Venda não existente");

            _dbContext.Transacao.Remove(vendaId);
            _dbContext.SaveChanges();

            return await Task.FromResult(id);

        }
        private void ValidarObrigatoriedade(Transacao transacao)
        {
            if (transacao.ProdutoId == 0)
                throw new Exception($"Campo Obrigatório");

            if (transacao.Quantidade == 0)
                throw new Exception($"Campo Obrigatório");
        }


    }
}
