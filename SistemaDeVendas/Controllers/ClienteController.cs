using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Entidades;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class ClienteController : Controller
    {
        //Injeção de dependência
        public readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Cliente> lista = _context.Cliente.ToList();
            //liberar memória
            _context.Dispose();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ClienteViewModel viewModel = new ClienteViewModel();
            if (id != null)
            {
                var entidade = _context.Cliente.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Nome = entidade.Nome;
                viewModel.CNPJ_CPF = entidade.CNPJ_CPF;
                viewModel.Email = entidade.Email;
                viewModel.Celular = entidade.Celular;   

            }


            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Cliente objCliente = new Cliente()
                {
                    Codigo = model.Codigo,
                    Nome = model.Nome,
                    CNPJ_CPF = model.CNPJ_CPF,
                    Email = model.Email,
                    Celular = model.Celular,
                };

                if (model.Codigo == null)
                {
                    _context.Cliente.Add(objCliente);
                }
                else
                {
                    _context.Entry(objCliente).State = EntityState.Modified;
                }
                _context.SaveChanges();

            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cat = new Cliente() { Codigo = id };
            _context.Attach(cat);
            _context.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

