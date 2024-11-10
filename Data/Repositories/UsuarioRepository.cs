using fornecedor_api.Data.Context;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Data.Repositories;

public class UsuarioRepository  : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(FornecedoresContext context) : base(context)
    {
    }

    public Usuario GetUsuarioByLoginAsync(string login, string senha)
    {
        var user = context.Usuarios.FirstOrDefault(user => user.Login == login && user.Senha == senha);
        return user;
    }

    public void AddUsuarioAsync(Usuario usuario)
    {
        context.Usuarios.Add(usuario);
    }
}