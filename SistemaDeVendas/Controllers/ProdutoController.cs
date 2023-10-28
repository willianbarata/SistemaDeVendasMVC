using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Entidades;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class ProdutoController : Controller
    {
        public readonly ApplicationDbContext _context;
        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Produto> lista = _context.Produto.Include(x => x.Categoria).ToList();
            //liberar memória
            _context.Dispose();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel();
            viewModel.ListaCategorias = ListaCategorias();
            if (id != null)
            {
                var entidade = _context.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
                viewModel.Quantidade = entidade.Quantidade;
                viewModel.Valor = entidade.Valor;
                viewModel.CodigoCategoria = entidade.CodigoCategoria;
            }
            return View(viewModel);
        }

        private IEnumerable<SelectListItem> ListaCategorias()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty,
            });

            foreach (var item in _context.Categoria.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString(),
                });
            }

            return lista;
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Produto objProduto = new Produto()
                {
                    Codigo = model.Codigo,
                    Descricao = model.Descricao,
                    Quantidade = model.Quantidade,
                    Valor = model.Valor,
                    CodigoCategoria = (int)model.CodigoCategoria,
                };

                if (model.Codigo == null)
                {
                    _context.Produto.Add(objProduto);
                }
                else
                {
                    _context.Entry(objProduto).State = EntityState.Modified;
                }
                _context.SaveChanges();

            }
            else
            {
                model.ListaCategorias = ListaCategorias();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cat = new Produto() { Codigo = id };
            _context.Attach(cat);
            _context.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
