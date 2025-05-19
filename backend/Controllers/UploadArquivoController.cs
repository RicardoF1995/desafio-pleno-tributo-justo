using backend.Business;
using backend.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadArquivoController : ControllerBase
    {
        private readonly UploadArquivoBusiness _uploadArquivoBusiness;

        public UploadArquivoController(UploadArquivoBusiness uploadArquivoBusiness)
        {
            _uploadArquivoBusiness = uploadArquivoBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> UploadArquivoAsync(IFormFile file)
        {
            try
            {
                using var stream = file.OpenReadStream();
                await _uploadArquivoBusiness.ProcessarArquivoCsvAsync(stream);
                return Ok("Arquivo processado com sucesso.");
            }
            catch (Exception ex)
            {
                // Logar erro aqui, se quiser
                return BadRequest($"Erro ao processar o arquivo: {ex.Message}");
            }
        }
    }
}
