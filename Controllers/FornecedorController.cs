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
    /// <summary>
    /// Construtor do controlador de fornecedores.
    /// </summary>
    /// <param name="fornecedorService">Serviço de fornecedores para interações com a lógica de negócios</param>
    /// <param name="mapper">Objeto AutoMapper para conversões entre DTOs e entidades</param>
    /// <param name="enderecoFornecedorService">Serviço de endereços de fornecedores</param>

    public FornecedorController(IFornecedorService fornecedorService, IMapper mapper, IEnderecoFornecedorService enderecoFornecedorService)
    {
        this.fornecedorService = fornecedorService;
        this.mapper = mapper;
        this.enderecoFornecedorService = enderecoFornecedorService;
    }
    /// <summary>
    /// Recupera todos os fornecedores cadastrados.
    /// </summary>
    /// <returns>Lista de fornecedores com sucesso ou NoContent caso não haja fornecedores.</returns>

    [HttpGet]
    public async Task<IActionResult> GetFornecedores()
    {
        var fornecedores = await fornecedorService.ListaFornecedores();
        var fornecedoresDto = mapper.Map<List<ReadFornecedorDto>>(fornecedores).ToList();
        if (!fornecedoresDto.Any())
            return NoContent();
        return Ok(fornecedoresDto);
    }
    /// <summary>
    /// Recupera um fornecedor pelo seu ID.
    /// </summary>
    /// <param name="id">ID do fornecedor a ser recuperado</param>
    /// <returns>Fornecedor encontrado ou NotFound se não existir.</returns>

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
    /// <summary>
    /// Cria um novo fornecedor.
    /// </summary>
    /// <param name="fornecedorDto">Objeto DTO com os dados do fornecedor a ser criado</param>
    /// <returns>Fornecedor criado com sucesso, com retorno do ID do novo fornecedor</returns>

    [HttpPost]
    public async Task<IActionResult> CreateFornecedor([FromBody] CreateFornecedorDto fornecedorDto)
    {
        var fornecedor = mapper.Map<Fornecedor>(fornecedorDto);
        await fornecedorService.CadastraFornecedor(fornecedor);
        return CreatedAtAction(nameof(GetFornecedoresById), new { id = fornecedor.Id }, fornecedor);
    }
    /// <summary>
    /// Atualiza um fornecedor existente.
    /// </summary>
    /// <param name="fornecedorDto">Objeto DTO com as informações a serem atualizadas</param>
    /// <param name="id">ID do fornecedor a ser atualizado</param>
    /// <returns>Retorna Ok se a atualização for bem-sucedida ou BadRequest se o fornecedor não for encontrado</returns>

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
    /// <summary>
    /// Exclui um fornecedor pelo seu ID.
    /// </summary>
    /// <param name="id">ID do fornecedor a ser excluído</param>
    /// <returns>Retorna NoContent em caso de sucesso ou BadRequest em caso de erro</returns>

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
    /// <summary>
    /// Adiciona um novo endereço a um fornecedor.
    /// </summary>
    /// <param name="createEnderecoDto">Objeto DTO com as informações do endereço a ser adicionado</param>
    /// <param name="id">ID do fornecedor ao qual o endereço será vinculado</param>
    /// <returns>Fornecedor atualizado com o novo endereço ou BadRequest caso o fornecedor não seja encontrado</returns>

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