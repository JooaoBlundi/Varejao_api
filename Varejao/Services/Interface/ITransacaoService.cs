using Microsoft.AspNetCore.Mvc;
using Varejao.Models;

namespace Varejao.Repositorios.Interface
{
    public interface ITransacaoService
    {
        Task<Transacao> Get(long id);
        Task<TransacaoModel> GetId(long id);
        Task<List<TransacaoModel>> GetAll();
        Task<long> Add(Transacao transacao);
        Task<long> Delete(long id);
    }
}
