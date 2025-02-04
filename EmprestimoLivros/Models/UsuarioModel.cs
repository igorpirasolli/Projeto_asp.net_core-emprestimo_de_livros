using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
    }
}
