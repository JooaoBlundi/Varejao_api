using System.ComponentModel.DataAnnotations;
using Varejao_api.Enums;

namespace Varejao.Models
{
    public class UsuarioModel
    {

        public UsuarioModel(long id, string nome, string email, string cpf, string cargo, string login, string senha)
        {
            this.Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Cargo = cargo;
            Login = login;
            Senha = senha;
        }

        public long Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public string Cargo { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

    }

}
