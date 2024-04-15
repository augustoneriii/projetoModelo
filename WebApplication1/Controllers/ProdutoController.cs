using app.BE;
using app.Data;
using app.DTO;
using app.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoBE _be;
        private AppDbContext _context;

        public ProdutoController(ProdutoBE be, AppDbContext context)
        {
            _be = be;
            _context = context;
        }

        // GET: Produto

        [Route("getAllProdutos")]
        [HttpGet]
        public async Task<IActionResult> GetAll(ProdutoDTO dto)
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

        [Route("insertProduto")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ProdutoRequest produto)
        {
            try
            {
                _context.BeginTransaction();
                var response = await _be.Insert(produto);
                _context.Commit();
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (_context.TransactionIsActive())
                {
                    _context.Rollback();
                }
                return BadRequest(ex.Message);
            }
        }

        [Route("updateProduto")]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] ProdutoRequest produto)
        {
            try
            {
                _context.BeginTransaction();
                var response = await _be.Update(produto);
                _context.Commit();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _context.Rollback();
                return BadRequest(ex.Message);
            }
        }

        [Route("deleteProduto")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
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
