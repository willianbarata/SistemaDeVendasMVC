using Microsoft.AspNetCore.Mvc;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Entidades;
using SistemaDeVendas.Helpers;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class LoginController : Controller
    {
        public readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["ErroLogin"] = string.Empty;

            if(ModelState.IsValid)
            {
                var senha = CriptografiaMD5.GetMd5Hash(model.Senha);
                model.Senha = senha;
                var usuario = _context.Usuario.Where(x => x.Email == model.Email && x.Senha == model.Senha).FirstOrDefault();

                if(usuario== null)
                {
                    ViewData["ErroLogin"] = "E-mail ou senha incorretos";
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(model);
            }
            
        }

    }
}
