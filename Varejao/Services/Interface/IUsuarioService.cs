using Microsoft.AspNetCore.Mvc;
using Varejao.Models;

namespace Varejao.Repositorios.Interface
{
    public interface IUsuarioService
    {
        Task<List<UsuarioModel>> GetAll();
        Task<Usuario> Get(long id);
        Task<UsuarioModel> GetId(long id);
        Task<long> Add(Usuario usuario);
        Task<long> Update(Usuario usuario, long id);
        Task<long> Delete(long id);
    }
}
