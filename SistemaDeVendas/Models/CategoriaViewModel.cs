using System.ComponentModel.DataAnnotations;

namespace SistemaDeVendas.Models
{
    public class CategoriaViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Informe a Descrição da Categoria!")]
        public string Descricao { get; set; }
    }
}
