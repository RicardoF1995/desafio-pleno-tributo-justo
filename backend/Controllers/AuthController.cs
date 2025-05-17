using Backend.Business;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Backend.Services; 

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UsuarioBusiness _usuarioBusiness;
    private readonly JwtService _jwtService;

    public AuthController(UsuarioBusiness usuarioBusiness, JwtService jwtService)
    {
        _usuarioBusiness = usuarioBusiness;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CriarUsuario([FromBody] UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var sucesso = await _usuarioBusiness.CadastrarUsuarioAsync(usuarioDTO);
        if (!sucesso)
            return BadRequest("Usuário já existe.");

        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUsuario([FromBody] UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var usuarioValido = await _usuarioBusiness.ValidarLoginAsync(usuarioDTO);
        if (usuarioValido == null)
            return Unauthorized("Usuário ou senha inválidos.");

        var token = _jwtService.GerarToken(usuarioValido.NomeUsuario);

        return Ok(new { token });
    }
}
