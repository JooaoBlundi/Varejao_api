using Microsoft.EntityFrameworkCore;
using System.Data;
using Varejao.Data;
using Varejao.Models;
using Varejao.Repositorios.Interface;
using System.Linq;


namespace Varejao.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly SistemaVarejaoDbContext _dbContext;

        public ProdutoService(SistemaVarejaoDbContext sistemaVarejaoDbContext) 
        { 
            _dbContext = sistemaVarejaoDbContext;
        }

        public async Task<Produto> Get(long id)
        {
            var usuario = await _dbContext.Produto.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
                throw new Exception($"Usuario não existente");

            return usuario;
        }

        public async Task<ProdutoModel> GetId(long id)
        {
            var produto = await _dbContext.Produto.FirstOrDefaultAsync(x => x.Id == id);

            if (produto == null)
                throw new Exception($"Produto não cadastrado");

            var produtoModel = new ProdutoModel(

                produto.Id,
                produto.Nome,
                produto.Categoria.ToString(),
                produto.Validade,
                produto.Quantidade,
                produto.PrecoCusto,
                produto.PrecoVenda
            );

            return produtoModel;
        }

        public async Task<List<ProdutoModel>> GetAll()
        {
            var produtos = await _dbContext.Produto.ToListAsync();

            var produtosModel = produtos.Select(a => new ProdutoModel(
                a.Id,
                a.Nome,
                a.Categoria.ToString(),
                a.Validade,
                a.Quantidade,
                a.PrecoCusto,
                a.PrecoVenda

            )).ToList();

            return produtosModel;
        }

        public async Task<long> Add(Produto produto)
        {
            ValidarObrigatoriedade(produto);

           await _dbContext.Produto.AddAsync(produto);
           await _dbContext.SaveChangesAsync();

            return await Task.FromResult(produto.Id);
        }

        public async Task<long> Update(Produto produto, long id)
        {
            var produtoExistente = await Get(id);

            if (produtoExistente == null)
            {
                throw new Exception($"Produto não existente com o ID: {id}");
            }

            // Atualiza apenas os campos necessários
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Categoria = produto.Categoria;
            produtoExistente.Validade = produto.Validade;
            produtoExistente.Quantidade = produto.Quantidade;
            produtoExistente.PrecoCusto = produto.PrecoCusto;
            produtoExistente.PrecoVenda = produto.PrecoVenda;


            _dbContext.Produto.Update(produtoExistente);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(id);

        }

        public async Task<long> Delete(long id)
        {

            var produtoId = await Get(id);

            if (produtoId == null)
                throw new Exception($"Usuario não existente");

            _dbContext.Produto.Remove(produtoId);
            _dbContext.SaveChanges();

            return await Task.FromResult(id);

        }

        private void ValidarObrigatoriedade(Produto produto)
        {
            if (produto.Nome == null)
                throw new Exception($"Campo Obrigatório");

            if (produto.Categoria == 0)
                throw new Exception($"Campo Obrigatório");

            if (produto.PrecoCusto == 0)
                throw new Exception($"Campo Obrigatório");

            if (produto.PrecoVenda == 0)
                throw new Exception($"Campo Obrigatório");

        }


    }
}
