namespace Varejao.Models
{
    public class TransacaoModel
    {
        public TransacaoModel(long id, string produtoId, int quantidade, string categoria) 
        {
            Id= id;
            NomeProduto = produtoId;   
            Quantidade= quantidade;
            Categoria= categoria;
        }
        
        public long Id { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public string Categoria { get; set; }
    }
}

