using backend.Business;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly AlertasBusiness _alertasBusiness;

        public AlertasController(AlertasBusiness alertasBusiness)
        {
            _alertasBusiness = alertasBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> RetornarAlertasAsync()
        {
            try
            {
                var relatorio = await _alertasBusiness.RetornarAlertasAsync();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar relatório: {ex.Message}");
            }
        }
    }
}
