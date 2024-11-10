using fornecedor_api.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace fornecedor_api.Interfaces;

public interface IUsuarioService
{
    public Task LoginAsync(Usuario usuario);
    
}