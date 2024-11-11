namespace fornecedor_api.Models.DTOs;

public class ReadEnderecoFornecedorDto
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public string Logradouro { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
}