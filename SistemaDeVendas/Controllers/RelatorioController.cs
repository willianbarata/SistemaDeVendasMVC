using Microsoft.AspNetCore.Mvc;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public readonly ApplicationDbContext _context;
        public RelatorioController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Grafico()
        {
            var lista = _context.VendaProdutos
                    .GroupBy(x => x.CodigoProduto)
                    .Select(y => new GraficoViewModel
                    {
                        CodigoProduto = y.First().CodigoProduto,
                        Descricao = y.First().Produto.Descricao,
                        TotalVendido = y.Sum(z => z.Quantidade)
                    }).ToList();

            string valores = string.Empty;
            string labels = string.Empty;
            string cores = string.Empty;
            var random = new Random();

            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].TotalVendido.ToString() + ",";
                labels += "'" + lista[i].Descricao.ToString() + "',";
                cores += "'" + String.Format("#{0:X6}" , random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;
            return View();
        }
    }
}
