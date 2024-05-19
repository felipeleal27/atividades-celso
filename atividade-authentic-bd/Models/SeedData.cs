using Microsoft.EntityFrameworkCore;


namespace Atividade3.Models
{
    public class SeedData
    {

        public static void EnsurePopulated(IApplicationBuilder app)
        {

            //associa dados ao contexto
            Contexto contexto = app.ApplicationServices.GetService<Contexto>();
            //inserir os dados das entidades do contexto
            contexto.Database.Migrate();
        }
        public static void semearDados(IApplicationBuilder appp)
        {

            Contexto contexto = appp.ApplicationServices.GetRequiredService<Contexto>();

            contexto.Database.Migrate();

            if (contexto.Alunos.Any() || contexto.Personals.Any() || contexto.Exercicios.Any() || contexto.Treinos.Any())
            {
                return;   // Dados já foram semeados
            }

            // Insere dados de teste para Aluno
            contexto.Alunos.AddRange(
                new Aluno
                {
                    Nome = "Celso",
                    Data_Nascimento = DateTime.Parse("25-05-1973"),
                    Email = "celso.ramos@unifenas.br",
                    Instagram = "@profcelsoavila",
                    Telefone = "35991197084",
                    Observacoes = "Dores no ombro esquerdo"
                },

                new Aluno
                {
                    Nome = "Maria",
                    Data_Nascimento = DateTime.Parse("13-3-2001"),
                    Email = "mariateste@gmail.com",
                    Instagram = "@mariateste",
                    Telefone = "35998748571",
                    Observacoes = "Cirurgia no tornozelo esquerdo"
                }
            );

            // Insere dados de teste para Personal
            contexto.Personals.AddRange(
                new Personal
                {
                    Nome = "Pedro Augusto",
                    Especialidade = "Body Build"
                },

                new Personal
                {
                    Nome = "Juliana Alves",
                    Especialidade = "Hipertrofia"
                }
            );

            // Insere dados de teste para Exercicio

            var exercicios = new List<Exercicio>
            {
                new Exercicio
                {
                    Nome = "Rosca direta",
                    Categoria = "Bíceps",
                    Descricao = "Exer´cio na máquina ou livre"
                },

                new Exercicio
                {
                    Nome = "Leg Press",
                    Categoria = "Membros inferiores",
                    Descricao = "Exercício para posterior de coxa"
                }
            };
            contexto.Exercicios.AddRange(exercicios);

            // Insere dados de teste para Treino
            contexto.Treinos.AddRange(
                new Treino
                {
                    PersonalID = 1,
                    AlunoID = 1,
                    Data = DateTime.Now,
                    Hora = DateTime.Now,

                    Exercicios = new List<Exercicio> {
                        new Exercicio{
                          Nome = "Rosca direta",
                          Categoria = "Bíceps",
                          Descricao = "Exercício na máquina ou livre"
                        },

                        new Exercicio{
                          Nome = "Leg Press",
                          Categoria = "Membros inferiores",
                          Descricao = "Exercício para posterior de coxa"
                       }
                    }
                },

                new Treino
                {
                    PersonalID = 2,
                    AlunoID = 2,
                    Data = DateTime.Now,
                    Hora = DateTime.Now,
                    Exercicios = new List<Exercicio> {
                        new Exercicio{
                          Nome = "Rosca scott",
                          Categoria = "Bíceps",
                          Descricao = "Execícioo na máquina ou livre"
                        },

                        new Exercicio{
                          Nome = "Panturrilha",
                          Categoria = "Membros inferiores",
                          Descricao = "Exercício sentado ou de pé"
                       }
                    }
                }
            );

            contexto.SaveChanges();
        }
    }
}