using fornecedor_api.Models.Entities;

namespace fornecedor_api.Interfaces;

public interface IEnderecoFornecedorService
{
    public Task AddAsync(EnderecoFornecedor enderecoFornecedor);
    public Task<List<EnderecoFornecedor>> GetByFornecedorIdAsync(int id);
}