using Atividade3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Atividade3.Controllers
{
    public class TreinoController : Controller
    {
        public Contexto context;
        public TreinoController(Contexto ctx)
        {
            context = ctx;
        }
        
        public IActionResult Index()
        {
            // Obtém todos os treinos do banco de dados
            var treinos = context.Treinos.Include(t => t.Exercicios)
            .Include(a => a.Aluno);
               // .ToList();

             return View(treinos);
            

        }


        public IActionResult Create()
        {
            ViewBag.Exercicios = new MultiSelectList(context.Exercicios.OrderBy(e => e.Nome), "ExercicioID", "Nome");
            ViewBag.AlunoID = new SelectList(context.Alunos.OrderBy(a => a.Nome), "AlunoID", "Nome");
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(p => p.Nome), "PersonalID", "Nome");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Treino treino, int[] exerciciosTreino)
        {
            treino.Exercicios = new List<Exercicio>();
            if (exerciciosTreino != null)
            {
                foreach (var e in exerciciosTreino)
                {
                    var exercicio = context.Exercicios.Find(e);
                    if (exercicio != null)
                    {
                        treino.Exercicios.Add(exercicio);
                    }
                }
            }

            context.Treinos.Add(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }




        public IActionResult Details(int id)
        {
            var treino = context.Treinos
                .Include(a => a.Aluno)
                .Include(p => p.Personal)
                .Include(e => e.Exercicios)
                .FirstOrDefault(t => t.TreinoID == id);
            return View(treino);
        }

        public IActionResult Edit(int id)
        {
            var treino = context.Treinos.Find(id);
            ViewBag.AlunoID = new SelectList(context.Alunos.OrderBy(a => a.AlunoID),
                "AlunoID", "Nome");
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(p => p.PersonalID),
                "PersonalID", "Nome");
            ViewBag.ExercicioID = new SelectList(context.Exercicios.OrderBy(e => e.ExercicioID),
                "ExercicioID", "Nome");
            return View(treino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Treino treino)
        {
            context.Entry(treino).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var treino = context.Treinos
                .Include(a => a.Aluno)
                .Include(p => p.Personal)
                .Include(e => e.Exercicios)
                .FirstOrDefault(e => e.TreinoID == id);
            return View(treino);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Treino treino)
        {
            context.Remove(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}