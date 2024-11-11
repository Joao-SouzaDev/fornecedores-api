namespace fornecedor_api.Models.Entities;

public class EnderecoFornecedor
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public string Logradouro { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Cep { get; set; }
    public virtual Fornecedor Fornecedor { get; set; }
}