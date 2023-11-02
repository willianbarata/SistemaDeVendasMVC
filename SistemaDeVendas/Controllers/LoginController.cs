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
        public readonly IHttpContextAccessor _httpSession;
        public LoginController(ApplicationDbContext context, IHttpContextAccessor httpSession)
        {
            _context = context;
            _httpSession = httpSession;
        }
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if(id == 0)
                {
                    _httpSession.HttpContext.Session.Clear();
                }
            }
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
                    _httpSession.HttpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.Nome);
                    _httpSession.HttpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.Email);
                    _httpSession.HttpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, (int)usuario.Codigo);
                    _httpSession.HttpContext.Session.SetInt32(Sessao.LOGADO, 1);
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
