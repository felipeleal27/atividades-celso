using System.ComponentModel.DataAnnotations;

namespace Atividade3.Models
{
    public class Treino
    {
        public int TreinoID { get; set; }
        public int PersonalID { get; set; }
        public int AlunoID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime Hora { get; set; }
        public Personal Personal { get; set; }
        public Aluno Aluno { get; set; }
        public ICollection<Exercicio> Exercicios { get; set; }
       
    }
}
