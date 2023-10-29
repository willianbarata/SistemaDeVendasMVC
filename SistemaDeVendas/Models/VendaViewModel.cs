using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVendas.Models
{
    public class VendaViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Informe a Data da Venda!")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe o cliente!")]
        public int? CodigoCliente { get; set; }
        public IEnumerable<SelectListItem> ListaClientes { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }
        public string JsonProdutos { get; set; }
    }
}
