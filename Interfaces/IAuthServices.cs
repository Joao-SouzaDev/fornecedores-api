using fornecedor_api.Models.Entities;

namespace fornecedor_api.Interfaces;

public interface IAuthServices
{
    public string GenerateToken(Usuario usuario);
}