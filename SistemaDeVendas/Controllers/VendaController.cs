using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Entidades;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class VendaController : Controller
    {
        public readonly ApplicationDbContext _context;
        public VendaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Venda> lista = _context.Venda.ToList();
            //liberar memória
            _context.Dispose();

            return View(lista);
        }
        private IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty,
            });

            foreach (var item in _context.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString(),
                });
            }

            return lista;
        }

        private IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty,
            });

            foreach (var item in _context.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString(),
                });
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            VendaViewModel viewModel = new VendaViewModel();
            viewModel.ListaClientes = ListaClientes();
            viewModel.ListaProdutos = ListaProdutos();
            if (id != null)
            {
                var entidade = _context.Venda.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Data= entidade.Data;
                viewModel.CodigoCliente = entidade.CodigoCliente;
                viewModel.Total = entidade.Total;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Venda objVenda = new Venda()
                {
                    Codigo = model.Codigo,
                    Data = model.Data,
                    CodigoCliente = (int)model.CodigoCliente,
                    Total = model.Total,
                    Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(model.JsonProdutos)

                };

                //objVenda.Cliente = _context.Cliente.Where(x => x.Codigo ==  model.CodigoCliente).FirstOrDefault();

                if (model.Codigo == null)
                {
                    _context.Venda.Add(objVenda);
                }
                else
                {
                    _context.Entry(objVenda).State = EntityState.Modified;
                }
                _context.SaveChanges();

            }
            else
            {
                model.ListaClientes = ListaClientes();
                model.ListaProdutos = ListaProdutos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cat = new Venda() { Codigo = id };
            _context.Attach(cat);
            _context.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto)
        {
            return _context.Produto.Where(x => x.Codigo == CodigoProduto).Select(x => x.Valor).FirstOrDefault();
        }


    }
}
