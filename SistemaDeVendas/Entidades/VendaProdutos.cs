namespace SistemaDeVendas.Entidades
{
    public class VendaProdutos
    {
        public int CodigoVenda { get; set; }
        public int CodigoProduto { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }

    }
}
