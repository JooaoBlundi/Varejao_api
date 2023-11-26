using System.Text.Json.Serialization;
using Varejao_api.Enums;

namespace Varejao.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public DateTimeOffset Validade { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        [JsonIgnore]
        public decimal Lucro { get; set; }
    }
}

