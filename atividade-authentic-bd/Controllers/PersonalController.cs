using Atividade3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Atividade3.Controllers
{
    public class PersonalController : Controller
    {
        public Contexto context;
        public PersonalController(Contexto ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View(context.Personals.Include(a => a.Alunos));
        }

        public IActionResult Create()
        {
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(p => p.Nome),
                "PersonalID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Personal personal)
        {
            context.Add(personal);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var personal = context.Personals
                .Include(a => a.Alunos)
                .FirstOrDefault(p => p.PersonalID == id);
            return View(personal);
        }

        public IActionResult Edit(int id)
        {
            var personal = context.Personals.Find(id);
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(a => a.Nome), "PersonalID", "Nome");
            return View(personal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Personal personal)
        {
            context.Entry(personal).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var personal = context.Personals
                .Include(a => a.Alunos)
                .FirstOrDefault(p => p.PersonalID == id);
            return View(personal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Personal personal)
        {
            IEnumerable<Aluno> alunosToUpdate = context.Alunos.Where(a => a.PersonalID == personal.PersonalID);
            foreach (var aluno in alunosToUpdate)
            {
                aluno.PersonalID = null;
            }

            context.Remove(personal);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
