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
            var relatorio = await _relatoriosBusiness.RetornarRelatorioAsync();
            return Ok(relatorio);
        }
    }
}
