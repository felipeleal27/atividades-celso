using System.ComponentModel.DataAnnotations;

namespace Atividade3.Models
{
    public class Usuarios
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string Nome { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Forneça e-mail válido")]
        public string Email { get; set; }
    }
}
