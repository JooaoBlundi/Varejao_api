using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Varejao_api.Enums;
using static Varejao.Models.UsuarioModel;

namespace Varejao.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public CargoEnum Cargo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}

