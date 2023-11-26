using System.Text.Json.Serialization;
using Varejao_api.Enums;

namespace Varejao.Models
{
    public class ProdutoModel
    {

        public ProdutoModel(long id, string nome, string categoria, DateTimeOffset validade, int quantidade, decimal precoVenda, decimal precoCusto)
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            Validade = validade;
            Quantidade = quantidade;
            PrecoVenda = precoVenda;
            PrecoCusto = precoCusto;
        }

        public long Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public DateTimeOffset Validade { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        [JsonIgnore]
        public decimal Lucro { get; set; }
    }
}

