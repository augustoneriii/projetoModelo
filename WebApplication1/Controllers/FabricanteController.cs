// Correção no Controller com injeção de dependência
using app.BE;
using app.Data;
using app.DTO;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Replication.PgOutput.Messages;

namespace app.Controllers
{
    public class FabricanteController : Controller
    {
        private FabricanteBE _be;
        private AppDbContext _context;

        public FabricanteController(FabricanteBE be, AppDbContext context)
        {
            _be = be;
            _context = context;
        }

        // GET: Fabricante
        [Route("getAllFabricantes")]
        [HttpGet]
        public async Task<IActionResult> GetAll(FabricanteDTO dto)
        {
            try
            {
                var response = await _be.GetAll(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("insertFabricante")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] FabricanteDTO fabricante)
        {
            try
            {
                _context.BeginTransaction();  
                var response = await _be.Insert(fabricante);
                _context.Commit();  
                return Ok(response);
            }
            catch (Exception ex)
            {
                _context.Rollback();  
                return BadRequest(ex.Message);
            }
        }

        [Route("updateFabricante")]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] FabricanteDTO fabricante)
        {
            try
            {
                _context.BeginTransaction();
                var response = await _be.Update(fabricante);
                _context.Commit();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _context.Rollback();
                return BadRequest(ex.Message);
            }
        }

        [Route("deleteFabricante")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                _context.BeginTransaction();
                await _be.Delete(id);
                _context.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                return BadRequest(ex.Message);
            }
        }
    }
}