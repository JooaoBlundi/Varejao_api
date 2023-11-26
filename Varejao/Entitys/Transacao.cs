using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Varejao_api.Enums;
using static Varejao.Models.UsuarioModel;

namespace Varejao.Models
{
    public class Transacao
    {
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public int Quantidade { get; set; }
        [JsonIgnore]
        public CategoriaEnum Categoria { get; set; }
        [JsonIgnore]
        public DateTime DataVenda { get; set; }
    }
}

