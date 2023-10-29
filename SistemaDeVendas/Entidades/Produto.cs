using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVendas.Entidades
{
    public class Produto
    {
        [Key]
        public int? Codigo { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public decimal Valor { get; set; }

        //DataAnnotations para chave estrangeira
        [ForeignKey("Categoria")]
        public int CodigoCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<VendaProdutos> Vendas { get; set; }
    }
}
