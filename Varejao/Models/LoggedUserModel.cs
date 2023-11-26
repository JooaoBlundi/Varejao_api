namespace Varejao.Models
{
    public class LoggedUserModel
    {
        public long Id { get; set; }
        public string Apelido { get; set; }
        public string Nome { get; set; }
        public string Nivel { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}

