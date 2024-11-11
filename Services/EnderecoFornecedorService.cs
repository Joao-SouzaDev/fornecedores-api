using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Services;

public class EnderecoFornecedorService : IEnderecoFornecedorService
{
    private IRepository<EnderecoFornecedor> _repository;
    public EnderecoFornecedorService(IRepository<EnderecoFornecedor> repository)
    {
        _repository = repository;
    }
    public async Task AddAsync(EnderecoFornecedor enderecoFornecedor)
    {
        await _repository.AddAsync(enderecoFornecedor);
    }

    public async Task<List<EnderecoFornecedor>> GetByFornecedorIdAsync(int id)
    {
        return (await _repository.GetAllAsync()).Where(e => e.FornecedorId == id).ToList();
    }
}