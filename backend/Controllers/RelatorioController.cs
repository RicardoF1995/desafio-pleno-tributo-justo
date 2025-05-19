using backend.Business;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly RelatoriosBusiness _relatoriosBusiness;

        public RelatoriosController(RelatoriosBusiness relatoriosBusiness)
        {
            _relatoriosBusiness = relatoriosBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> RetornarRelatorioAsync()
        {
            try
            {
                var relatorio = await _relatoriosBusiness.RetornarRelatorioAsync();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar relatório: {ex.Message}");
            }
        }
    }
}
