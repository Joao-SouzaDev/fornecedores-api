using fornecedor_api.Models.Entities;

namespace fornecedor_api.Models.DTOs;

public class ReadFornecedorDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public List<ReadEnderecoFornecedorDto> Endereco { get; set; }
}