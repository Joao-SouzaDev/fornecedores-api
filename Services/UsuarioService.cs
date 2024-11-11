using AutoMapper;
using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Services;

public class UsuarioService : IUsuarioService
{
    private  IAuthServices _authServices;
    private readonly IRepository<Usuario> repository;
    public UsuarioService(IAuthServices authServices, IRepository<Usuario> repository)
    {
        _authServices = authServices;
        this.repository = repository;
    }
    public async Task<string> LoginAsync(Usuario usuario)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x=> x.Login == usuario.Login && x.Senha== usuario.Senha);
        if (user == null)
            throw new Exception("Usuário ou senha incorretos! Tente novamente.");
        string token = _authServices.GenerateToken(user);
        return token;
    }

    public async Task CriarAsync(Usuario usuario)
    {
        if(usuario is null)
            throw new ArgumentNullException(nameof(usuario));
        await repository.AddAsync(usuario);
    }
}