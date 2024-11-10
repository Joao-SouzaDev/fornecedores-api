using AutoMapper;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.DTOs;
using fornecedor_api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fornecedor_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FornecedorController : ControllerBase
{
    private IFornecedorService fornecedorService;
    private IMapper mapper;
    public FornecedorController(IFornecedorService fornecedorService, IMapper mapper)
    {
        this.fornecedorService = fornecedorService;
        this.mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetFornecedores()
    {
        var fornecedores = await fornecedorService.ListaFornecedores();
        var fornecedoresDto = mapper.Map<List<ReadFornecedorDto>>(fornecedores).ToList();
        if (!fornecedoresDto.Any())
            return NoContent();
        return Ok(fornecedoresDto);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFornecedoresById(int id)
    {
        var fornecedor = await fornecedorService.BuscarFornecedorPorId(id);
        if (fornecedor == null)
            return NotFound();
        var fornecedorDto = mapper.Map<ReadFornecedorDto>(fornecedor);
        return Ok(fornecedorDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFornecedor([FromBody] CreateFornecedorDto fornecedorDto)
    {
        var fornecedor = mapper.Map<Fornecedor>(fornecedorDto);
        await fornecedorService.CadastraFornecedor(fornecedor);
        return CreatedAtAction(nameof(GetFornecedoresById), new { id = fornecedor.Id }, fornecedor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFornecedor([FromBody] CreateFornecedorDto fornecedorDto, int id)
    {
        var fornecedor = await fornecedorService.BuscarFornecedorPorId(id);
        if (fornecedor == null)
            return BadRequest("Fornecedor não encontrado!");
        mapper.Map(fornecedorDto, fornecedor);
        await fornecedorService.AtualizaFornecedor(fornecedor);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFornecedor(int id)
    {
        try
        {
            await fornecedorService.ExcluirFornecedor(id);
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

}