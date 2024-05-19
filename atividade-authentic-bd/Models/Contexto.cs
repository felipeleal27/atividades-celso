using Microsoft.EntityFrameworkCore;

namespace Atividade3.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; } 

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Treino> Treinos { get; set; }

        //configurar a relação muitos para muitos entre Treino e Exercício
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configuraçãoda entidade Treino
            modelBuilder.Entity<Treino>()
                //Especifica que um Treino tem muitos Exercícios
                .HasMany(t => t.Exercicios)
                //Especifica que um exercício pode ter muitos treinos
                .WithMany(e => e.Treinos)
                //Especifica a tabela de junção
                .UsingEntity(j => j.ToTable("ExercicioTreino"));
        }

    }
}
