using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVendas.Models
{
    public class ProdutoViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Informe a Descrição da Categoria!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade em estoque do produto!")]
        public double Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o valor unitário do produto!")]
        [Range(0.1, Double.PositiveInfinity)]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Informe a Categoria do produto!")]
        public int? CodigoCategoria { get; set; }
        
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }

    }
}
