namespace fornecedor_api.Models.DTOs;

public class CreateEnderecoFornecedorDto
{
    public string Logradouro { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
}