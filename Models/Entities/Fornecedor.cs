using System.Text.Json.Serialization;

namespace fornecedor_api.Models.Entities;

public class Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public virtual List<EnderecoFornecedor> Endereco { get; set; }
    
}