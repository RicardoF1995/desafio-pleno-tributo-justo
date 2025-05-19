using backend.Business;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstatisticasController : ControllerBase
    {
        private readonly EstatisticasBusiness _estatisticasBusiness;

        public EstatisticasController(EstatisticasBusiness estatisticasBusiness)
        {
            _estatisticasBusiness = estatisticasBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> RetornarEstatisticasAsync()
        {
            var estatisticas = await _estatisticasBusiness.RetornarEstatisticasAsync();
            return Ok(estatisticas);
        }
    }
}