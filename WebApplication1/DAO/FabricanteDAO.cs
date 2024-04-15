using app.DTO;
using System.Data;
using Npgsql; 
using app.Data;
using System.Text;

namespace app.DAO
{
    public class FabricanteDAO
    {
        private AppDbContext _context;

        public FabricanteDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FabricanteDTO>> GetAll(FabricanteDTO dto)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT \"IdFabricante\", \"Nome\", \"Endereco\", \"Telefone\" ");
            objSelect.Append("FROM public.\"Fabricante\"");
            objSelect.Append("WHERE 1 = 1 ");

            if (dto.IdFabricante > 0)
            {
                objSelect.Append($"AND \"IdFabricante\" = {dto.IdFabricante} ");
            }
            if (!string.IsNullOrEmpty(dto.Nome))
            {
                objSelect.Append($"AND \"Nome\" = '{dto.Nome}' ");
            }
            if (!string.IsNullOrEmpty(dto.Endereco))
            {
                objSelect.Append($"AND \"Endereco\" = '{dto.Endereco}' ");
            }
            if (!string.IsNullOrEmpty(dto.Telefone))
            {
                objSelect.Append($"AND \"Telefone\" = '{dto.Telefone}' ");
            }

            var dt = _context.ExecuteQuery(objSelect.ToString());

            var lstFabricante = new List<FabricanteDTO>();

            foreach (DataRow row in dt.Rows)
            {
                lstFabricante.Add(new FabricanteDTO
                {
                    IdFabricante = Convert.ToInt32(row["IdFabricante"]),
                    Nome = row["Nome"].ToString(),
                    Endereco = row["Endereco"].ToString(),
                    Telefone = row["Telefone"].ToString()
                });
            }
            return lstFabricante;
        }

        public async Task<FabricanteDTO> Insert(FabricanteDTO fabricante)
        {
            var objInsert = new StringBuilder();
            objInsert.Append("INSERT INTO public.\"Fabricante\" ");
            objInsert.Append("(\"Nome\", \"Endereco\", \"Telefone\") ");
            objInsert.Append("VALUES ");
            objInsert.Append($"('{fabricante.Nome}', '{fabricante.Endereco}', '{fabricante.Telefone}') ");
            objInsert.Append("RETURNING \"IdFabricante\";");

            var id = _context.ExecuteNonQuery(objInsert.ToString());
            fabricante.IdFabricante = id;

            return fabricante;
        }

        public async Task<FabricanteDTO> Update(FabricanteDTO fabricante)
        {
            var objUpdate = new StringBuilder();
            objUpdate.Append("UPDATE public.\"Fabricante\" ");
            objUpdate.Append("SET ");
            objUpdate.Append($"\"Nome\" = '{fabricante.Nome}', ");
            objUpdate.Append($"\"Endereco\" = '{fabricante.Endereco}', ");
            objUpdate.Append($"\"Telefone\" = '{fabricante.Telefone}' ");
            objUpdate.Append($"WHERE \"IdFabricante\" = {fabricante.IdFabricante};");

            _context.ExecuteNonQuery(objUpdate.ToString());

            return fabricante;
        }

        public async Task Delete(int id)
        {
            var objDelete = new StringBuilder();
            objDelete.Append("DELETE FROM public.\"Fabricante\" ");
            objDelete.Append($"WHERE \"IdFabricante\" = {id};");

            _context.ExecuteNonQuery(objDelete.ToString());
        }
    }
}