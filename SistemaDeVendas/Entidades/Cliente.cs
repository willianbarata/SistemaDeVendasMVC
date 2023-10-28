using System.ComponentModel.DataAnnotations;

namespace SistemaDeVendas.Entidades
{
    public class Cliente
    {
        [Key]
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        //Lista de venda que meu cliente possui
        public ICollection<Venda> Vendas { get; set; }

    }
}
