using app.DAO;
using app.Data;
using app.DTO;
using app.DTO.Request;

namespace app.BE
{
    public class ProdutoBE
    {
        private AppDbContext _context;

        public ProdutoBE(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoDTO>> GetAll(ProdutoDTO dto)
        {
            var dao = new ProdutoDAO(_context);
            return await dao.GetAll(dto);
        }

        public async Task<ProdutoRequest> Insert(ProdutoRequest produto)
        {
            var dao = new ProdutoDAO(_context);
            return await dao.Insert(produto);
        }

        public async Task<ProdutoRequest> Update(ProdutoRequest produto)
        {
            var dao = new ProdutoDAO(_context);
            return await dao.Update(produto);
        }

        public async Task Delete(int id)
        {
            var dao = new ProdutoDAO(_context);
            await dao.Delete(id);
        }
    }
}
