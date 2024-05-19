using Atividade3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Atividade3.Controllers
{
    public class SessionsController : Controller
    {
        private readonly Contexto _dbContext;

        public SessionsController(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var usuarios = _dbContext.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuarios usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var confirma = _dbContext.Usuarios.FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
            if (confirma != null)
            {
                HttpContext.Session.SetString("usuario_sessions", confirma.Nome);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Correto()
        {
            return View();
        }
    }
}
