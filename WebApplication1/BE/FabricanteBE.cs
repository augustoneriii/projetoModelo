using app.DAO;
using app.Data;
using app.DTO;

namespace app.BE
{
    public class FabricanteBE
    {
        private AppDbContext _context;

        public FabricanteBE(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FabricanteDTO>> GetAll(FabricanteDTO dto)
        {
            var dao = new FabricanteDAO(_context);
            return await dao.GetAll(dto);
        }

        public async Task<FabricanteDTO> Insert(FabricanteDTO fabricante)
        {
            var dao = new FabricanteDAO(_context);
            return await dao.Insert(fabricante);
        }

        public async Task<FabricanteDTO> Update(FabricanteDTO fabricante)
        {
            var dao = new FabricanteDAO(_context);
            return await dao.Update(fabricante);
        }

        public async Task Delete(int id)
        {
            var dao = new FabricanteDAO(_context);
            await dao.Delete(id);
        }
    }
}