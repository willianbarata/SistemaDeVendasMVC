using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVendas.Entidades
{
    public class Venda
    {
        [Key]
        public int? Codigo { get; set; }
        public DateTime Data { get; set; }

        [ForeignKey("Cliente")]
        public int CodigoCliente { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Total { get; set; }

        //Todos produtos registrados na venda, estão na VendaProdutos
        public ICollection<VendaProdutos> Produtos { get; set; }

        
    }
}
