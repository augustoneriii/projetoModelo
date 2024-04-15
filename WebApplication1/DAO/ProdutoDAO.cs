using app.Data;
using app.DTO;
using System.Data;
using System.Text;
using System.Collections.Generic;
using app.DTO.Request;
using Npgsql;

namespace app.DAO
{
    public class ProdutoDAO
    {
        private AppDbContext _context;

        public ProdutoDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoDTO>> GetAll(ProdutoDTO dto)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT prod.\"IdProduto\", prod.\"Nome\" AS \"NomeProduto\", prod.\"Preco\", prod.\"Quantidade\", ");
            objSelect.Append("fab.\"IdFabricante\", fab.\"Nome\" AS \"NomeFabricante\", fab.\"Endereco\", fab.\"Telefone\" ");
            objSelect.Append("FROM public.\"Produto\" prod ");
            objSelect.Append("LEFT JOIN public.\"Fabricante\" fab ON prod.\"IdFabricante\" = fab.\"IdFabricante\"");
            objSelect.Append("WHERE 1 = 1 ");

            if (dto.IdProduto > 0)
            {
                objSelect.Append($"AND prod.\"IdProduto\" = {dto.IdProduto} ");
            }
            if (!string.IsNullOrEmpty(dto.Nome))
            {
                objSelect.Append($"AND prod.\"Nome\" = '{dto.Nome}' ");
            }
            if (dto.Preco > 0)
            {
                objSelect.Append($"AND prod.\"Preco\" = {dto.Preco} ");
            }
            if (dto.Quantidade > 0)
            {
                objSelect.Append($"AND prod.\"Quantidade\" = {dto.Quantidade} ");
            }
            if (dto.Fabricante.IdFabricante > 0)
            {
                objSelect.Append($"AND fab.\"IdFabricante\" = {dto.Fabricante.IdFabricante} ");
            }
            if (!string.IsNullOrEmpty(dto.Fabricante.Nome))
            {
                objSelect.Append($"AND fab.\"Nome\" = '{dto.Fabricante.Nome}' ");
            }
            if (!string.IsNullOrEmpty(dto.Fabricante.Endereco))
            {
                objSelect.Append($"AND fab.\"Endereco\" = '{dto.Fabricante.Endereco}' ");
            }
            if (!string.IsNullOrEmpty(dto.Fabricante.Telefone))
            {
                objSelect.Append($"AND fab.\"Telefone\" = '{dto.Fabricante.Telefone}' ");
            }


            DataTable dt = _context.ExecuteQuery(objSelect.ToString());
            var produtos = new List<ProdutoDTO>();

            foreach (DataRow row in dt.Rows)
            {
                produtos.Add(new ProdutoDTO
                {
                    IdProduto = Convert.ToInt32(row["IdProduto"]),
                    Nome = row["NomeProduto"].ToString(),
                    Preco = Convert.ToDecimal(row["Preco"]),
                    Quantidade = Convert.ToInt32(row["Quantidade"]),
                    Fabricante = new FabricanteDTO
                    {
                        IdFabricante = row["IdFabricante"] != DBNull.Value ? Convert.ToInt32(row["IdFabricante"]) : 0,
                        Nome = row["NomeFabricante"].ToString(),
                        Endereco = row["Endereco"].ToString(),
                        Telefone = row["Telefone"].ToString()
                    }
                });
            }

            return produtos;
        }

        public async Task<ProdutoRequest> Insert(ProdutoRequest fabricante)
        {
            var objInsert = new StringBuilder();
            objInsert.Append("INSERT INTO public.\"Produto\" ");
            objInsert.Append("(\"Nome\", \"Preco\", \"Quantidade\", \"IdFabricante\") ");
            objInsert.Append("VALUES ");
            objInsert.Append($"('{fabricante.Nome}', {fabricante.Preco}, {fabricante.Quantidade}, {fabricante.IdFabricante}) ");
            objInsert.Append("RETURNING \"IdProduto\";");

            var id = _context.ExecuteNonQuery(objInsert.ToString());
            fabricante.IdFabricante = id;

            return fabricante;
        }


        public async Task<ProdutoRequest> Update(ProdutoRequest produto)
        {
            var objUpdate = new StringBuilder();
            objUpdate.Append("UPDATE public.\"Produto\" ");
            objUpdate.Append("SET ");
            objUpdate.Append($"\"Nome\" = '{produto.Nome}', ");
            objUpdate.Append($"\"Preco\" = {produto.Preco}, ");
            objUpdate.Append($"\"Quantidade\" = {produto.Quantidade}, ");
            objUpdate.Append($"\"IdFabricante\" = {produto.IdFabricante} ");
            objUpdate.Append($"WHERE \"IdProduto\" = {produto.IdProduto};");

            _context.ExecuteNonQuery(objUpdate.ToString());

            return produto;
        }

        public async Task Delete(int id)
        {
            var objDelete = new StringBuilder();
            objDelete.Append("DELETE FROM public.\"Produto\" ");
            objDelete.Append($"WHERE \"IdProduto\" = {id};");

            _context.ExecuteNonQuery(objDelete.ToString());
        }
    }
}
