using Microsoft.EntityFrameworkCore;
using System.Data;
using Varejao.Data;
using Varejao.Models;
using Varejao.Repositorios.Interface;
using System.Linq;


namespace Varejao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SistemaVarejaoDbContext _dbContext;

        public UsuarioService(SistemaVarejaoDbContext sistemaVarejaoDbContext) 
        { 
            _dbContext = sistemaVarejaoDbContext;
        }

        public async Task<Usuario> Get(long id)
        {
            var usuario = await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
                throw new Exception($"Usuario não existente");

            return usuario;
        }

        public async Task<UsuarioModel> GetId(long id)
        {
            var usuario = await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
                throw new Exception($"Usuario não existente");

            var usuarioModel = new UsuarioModel(

                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Cpf,
                usuario.Cargo.ToString(),
                usuario.Login,
                usuario.Senha
            );

            return usuarioModel;
        }

        public async Task<List<UsuarioModel>> GetAll()
        {
            var usuarios = await _dbContext.Usuario.ToListAsync();

            var produtosModel = usuarios.Select(a => new UsuarioModel(
                a.Id,
                a.Nome,
                a.Email,
                a.Cpf,
                a.Cargo.ToString(),
                a.Login,
                a.Senha

            )).ToList();

            return produtosModel;
        }

        public async Task<long> Add(Usuario usuario)
        {
            ValidarObrigatoriedade(usuario);

           await _dbContext.Usuario.AddAsync(usuario);
           await _dbContext.SaveChangesAsync();

            return await Task.FromResult(usuario.Id);
        }

        public async Task<long> Update(Usuario usuario, long id)
        {
            var usuarioExistente = await Get(id);

            if(usuarioExistente == null) 
                throw new Exception($"Usuario não existente");

            ValidarObrigatoriedade(usuario);

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Cpf = usuario.Cpf;
            usuarioExistente.Cargo = usuario.Cargo;
            usuarioExistente.Login = usuario.Login;
            usuarioExistente.Senha = usuario.Senha;

            _dbContext.Usuario.Update(usuarioExistente);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(id);

        }

        public async Task<long> Delete(long id)
        {

            var usuarioId = await Get(id);

            if (usuarioId == null)
                throw new Exception($"Usuario não existente");

            _dbContext.Usuario.Remove(usuarioId);
            _dbContext.SaveChanges();

            return await Task.FromResult(id);

        }

        private void ValidarObrigatoriedade(Usuario usuario)
        {
            if (usuario.Nome == null)
                throw new Exception($"Campo Obrigatório");

            if (usuario.Email == null)
                throw new Exception($"Campo Obrigatório");

            if (usuario.Cargo == 0)
                throw new Exception($"Campo Obrigatório");

            if (usuario.Cpf == null)
                throw new Exception($"Campo Obrigatório");

            if (usuario.Login == null)
                throw new Exception($"Campo Obrigatório");

            if (usuario.Senha == null)
                throw new Exception($"Campo Obrigatório");

        }


    }
}
