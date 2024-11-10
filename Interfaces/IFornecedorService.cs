using fornecedor_api.Models.Entities;

namespace fornecedor_api.Interfaces;

public interface IFornecedorService
{
    public Task CadastraFornecedor(Fornecedor fornecedor);
    public Task AtualizaFornecedor(Fornecedor fornecedor);
    public Task<Fornecedor?> BuscarFornecedorPorId(int id);
    public Task<List<Fornecedor>> ListaFornecedores();
    public void ExcluirFornecedor(int id);
}