using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Services;

public class FornecedorService : IFornecedorService
{
    private IRepository<Fornecedor> repository;

    public FornecedorService(IRepository<Fornecedor> repository)
    {
        this.repository = repository;
    }
    public async Task CadastraFornecedor(Fornecedor fornecedor)
    {
        if(fornecedor is null)
            throw new ArgumentNullException(nameof(fornecedor));
        await repository.AddAsync(fornecedor);
        
    }

    public async Task AtualizaFornecedor(Fornecedor fornecedor)
    {
        throw new NotImplementedException();
    }

    public async Task<Fornecedor?> BuscarFornecedorPorId(int id)
    {
        Fornecedor? fornecedor = await repository.GetByIdAsync(id);
        return fornecedor;
    }

    public async Task<List<Fornecedor>> ListaFornecedores()
    {
        List<Fornecedor> fornecedores = (await repository.GetAllAsync()).ToList();
        return fornecedores;
    }

    public void ExcluirFornecedor(int id)
    {
        throw new NotImplementedException();
    }
}