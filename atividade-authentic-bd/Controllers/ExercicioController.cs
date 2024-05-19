using Microsoft.AspNetCore.Mvc;
using Atividade3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Atividade3.Controllers
{
    public class ExercicioController : Controller
    {
        public Contexto context;
        public ExercicioController(Contexto ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View(context.Exercicios/*.Include(t => t.Treinos)*/);
        }

        public IActionResult Create()
        {
            //ViewBag.ExercicioID = new SelectList(context.Exercicios.OrderBy(e => e.Nome),
            //    "ExercicioID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exercicio exercicio)
        {
            context.Add(exercicio);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var exercicio = context.Exercicios
                .Include(t => t.Treinos)
                .FirstOrDefault(e => e.ExercicioID == id);
            return View(exercicio);
        }

        public IActionResult Edit(int id)
        {
            var exercicio = context.Exercicios.Find(id);
            ViewBag.ExercicioID = new SelectList(context.Exercicios.OrderBy(e => e.Nome),
                "ExercicioID", "Nome");
            return View(exercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Exercicio exercicio)
        {
            context.Entry(exercicio).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var exercicio = context.Exercicios
                .Include(t => t.Treinos)
                .FirstOrDefault(e => e.ExercicioID == id);
            return View(exercicio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Exercicio exercicio)
        {
            context.Remove(exercicio);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
