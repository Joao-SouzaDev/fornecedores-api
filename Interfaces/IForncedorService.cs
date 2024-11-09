using fornecedor_api.Models.Entities;

namespace fornecedor_api.Interfaces;

public interface IForncedorService
{
    public void CadastraFornecedor(Fornecedor fornecedor);
    public void AtualizaFornecedor(Fornecedor fornecedor);
    public Fornecedor BuscarFornecedorPorId(int id);
    public List<Fornecedor> ListaFornecedores();
    public void ExcluirFornecedor(int id);
}