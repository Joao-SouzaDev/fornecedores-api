using fornecedor_api.Models.Entities;

namespace fornecedor_api.Data.Repositories;

public interface IUsuarioRepository
{
    public Usuario GetUsuarioByLoginAsync(string login, string senha);
    public void AddUsuarioAsync(Usuario usuario);
}