using AutoMapper;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.DTOs;
using fornecedor_api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace fornecedor_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private IUsuarioService _usuarioService;
    
    private IMapper _mapper;

    public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
    }

    [HttpPost("Autenticar")]
    public async Task<IActionResult> Autenticar([FromBody] CreateUsuarioDto usuario)
    {
        var user = _mapper.Map<Usuario>(usuario);
        string token = await _usuarioService.LoginAsync(user);
        var usuarioAutenticado = _mapper.Map<UsuarioAutenticadoDto>(user);
        usuarioAutenticado.Token = token;
        return Ok(usuarioAutenticado);
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarUsuario([FromBody] CreateUsuarioDto usuario)
    {
        var user = _mapper.Map<Usuario>(usuario);
        await _usuarioService.CriarAsync(user);
        return Created("", usuario);
    }
}