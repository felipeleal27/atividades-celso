using Atividade3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace Atividade3.Controllers
{
    public class AlunoController : Controller
    {
        public Contexto contexto;
        public AlunoController(Contexto ctx)
        {
            contexto = ctx;
        }

        public IActionResult Index()
        {
            return View(contexto.Alunos.Include(p => p.Personals));
        }

        public IActionResult Create()
        {
            ViewBag.PersonalID = new SelectList(contexto.Personals.OrderBy(p => p.Nome),
                "PersonalID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aluno aluno)
        {
            contexto.Add(aluno);
            contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var aluno = contexto.Alunos
                .Include(p => p.Personals)
                .FirstOrDefault(a => a.AlunoID == id);
            return View(aluno);
        }

        public IActionResult Edit(int id)
        {
            var aluno = contexto.Alunos.Find(id);
            ViewBag.PersonalID = new SelectList(contexto.Personals.OrderBy(p => p.Nome),
                "PersonalID", "Nome");
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Aluno aluno)
        {
            contexto.Entry(aluno).State = EntityState.Modified;
            contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var aluno = contexto.Alunos
                .Include(p => p.Personals)
                .FirstOrDefault(a => a.AlunoID == id);
            return View(aluno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Aluno aluno)
        {
            contexto.Remove(aluno);
            contexto.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
