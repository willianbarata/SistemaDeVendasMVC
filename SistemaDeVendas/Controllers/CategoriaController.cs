using Microsoft.AspNetCore.Mvc;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Entidades;

namespace SistemaDeVendas.Controllers
{
    public class CategoriaController : Controller
    {
        //Injeção de dependência
        public readonly ApplicationDbContext _context;
        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = _context.Categoria.ToList();
            //liberar memória
            _context.Dispose();

            return View();
        }
    }
}
