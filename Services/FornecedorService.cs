using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fornecedor_api.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IRepository<Fornecedor> _repository;

    public FornecedorService(IRepository<Fornecedor> repository)
    {
        this._repository = repository;
    }
    public async Task CadastraFornecedor(Fornecedor fornecedor)
    {
        if(fornecedor is null)
            throw new ArgumentNullException(nameof(fornecedor));
        await _repository.AddAsync(fornecedor);
    }

    public async Task AtualizaFornecedor(Fornecedor fornecedor)
    {
        try
        {
            await _repository.UpdateAsync(fornecedor);
        }
        catch (Exception exception)
        {
            throw  new Exception(exception.Message);
        }
        
    }

    public async Task<Fornecedor?> BuscarFornecedorPorId(int id)
    {
        Fornecedor? fornecedor = await _repository.GetByIdAsync(id);
        return fornecedor;
    }

    public async Task<List<Fornecedor>> ListaFornecedores()
    {
        List<Fornecedor> fornecedores = (await _repository.GetAllAsync()).ToList();
        return fornecedores;
    }

    public async Task ExcluirFornecedor(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch (DbUpdateException exception)
        {
            throw  new Exception(exception.Message);
        }
    }
}