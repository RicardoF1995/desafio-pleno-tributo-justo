using backend.Business;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using backend.Services; 

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthBusiness _authBusiness;
    private readonly JwtService _jwtService;

    public AuthController(AuthBusiness authBusiness, JwtService jwtService)
    {
        _authBusiness = authBusiness;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CriarUsuario([FromBody] UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var sucesso = await _authBusiness.CadastrarUsuarioAsync(usuarioDTO);
        if (!sucesso)
            return BadRequest("Usuário já existe.");

        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUsuario([FromBody] UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuarioValido = await _authBusiness.ValidarLoginAsync(usuarioDTO);
        if (usuarioValido == null)
            return Unauthorized("Usuário ou senha inválidos.");

        var token = _jwtService.GerarToken(usuarioValido.NomeUsuario);

        return Ok(new { token });
    }
}
