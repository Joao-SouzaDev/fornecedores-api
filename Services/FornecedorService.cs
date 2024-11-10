using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            await repository.UpdateAsync(fornecedor);
        }
        catch (Exception exception)
        {
            throw  new Exception(exception.Message);
        }
        
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

    public async Task ExcluirFornecedor(int id)
    {
        try
        {
            await repository.DeleteAsync(id);
        }
        catch (DbUpdateException exception)
        {
            throw  new Exception(exception.Message);
        }
    }
}