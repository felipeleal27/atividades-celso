using Atividade3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Atividade3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _dbContext;

        public HomeController(ILogger<HomeController> logger, Contexto dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuarios usuario)
        {
            try
            {
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                // Log the exception or handle it appropriately
                // For debugging purposes, you can return the exception message
                return Content($"An error occurred: {ex.Message}");
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var confirma = _dbContext!.Usuarios.FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
            if (confirma != null)
            {
                HttpContext.Session.SetString("usuario_sessions", confirma.Nome);
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "Credenciais inválidas. Por favor, tente novamente.";
                return View();
            }
        }

    }
}