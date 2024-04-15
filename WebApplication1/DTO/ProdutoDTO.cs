namespace app.DTO
{
    public class ProdutoDTO
    {
        public long IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Quantidade { get; set; }

        public FabricanteDTO Fabricante { get; set; }
    }
}
