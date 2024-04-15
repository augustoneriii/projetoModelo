namespace app.DTO.Request
{
    public class ProdutoRequest
    {
        public long IdProduto { get; set; }
        public long IdFabricante { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Quantidade { get; set; }
    }
}
