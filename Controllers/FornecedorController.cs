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
    private IEnderecoFornecedorService enderecoFornecedorService;
    private IMapper mapper;
    public FornecedorController(IFornecedorService fornecedorService, IMapper mapper, IEnderecoFornecedorService enderecoFornecedorService)
    {
        this.fornecedorService = fornecedorService;
        this.mapper = mapper;
        this.enderecoFornecedorService = enderecoFornecedorService;
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
        var enderecos =await enderecoFornecedorService.GetByFornecedorIdAsync(fornecedor.Id);
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
    public async Task<IActionResult> UpdateFornecedor([FromBody] UpdateFornecedorDto fornecedorDto, int id)
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
            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

    [HttpPut("{id}/endereco")]
    public async Task<IActionResult> AdicionaEndereco([FromBody]CreateEnderecoFornecedorDto createEnderecoDto,int id)
    {
        var fornecedor = await fornecedorService.BuscarFornecedorPorId(id);
        if (fornecedor == null)
            return BadRequest("Fornecedor não encontrado");
        var endereco = mapper.Map<EnderecoFornecedor>(createEnderecoDto);
        endereco.FornecedorId = fornecedor.Id;
        await enderecoFornecedorService.AddAsync(endereco);
        var readFornecedor = mapper.Map<ReadFornecedorDto>(fornecedor);
        return Ok(readFornecedor);
    }

}