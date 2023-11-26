using Varejao.Models;

namespace Varejao.Repositorios.Interface
{
    public interface IProdutoService
    {
        Task<List<ProdutoModel>> GetAll();
        Task<Produto> Get(long id);
        Task<ProdutoModel> GetId(long id);
        Task<long> Add(Produto produto);
        Task<long> Update(Produto produto, long id);
        Task<long> Delete(long id);
    }
}
