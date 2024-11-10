using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IAuthServices _authServices;
    private  readonly IUsuarioRepository _repository;
    public UsuarioService(IAuthServices authServices, IUsuarioRepository repository)
    {
        this._authServices = authServices;
        this._repository = repository;
    }
    public string LoginAsync(Usuario usuario)
    {
        var user = _repository.GetUsuarioByLoginAsync(usuario.Login, usuario.Senha);
        if (user == null)
            throw new Exception("Usuário ou senha incorretos! Tente novamente.");
        string token = _authServices.GenerateToken(user);
        return token;
    }
}