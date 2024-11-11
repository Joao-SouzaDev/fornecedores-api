using System.ComponentModel.DataAnnotations;

namespace fornecedor_api.Models.DTOs;

public class CreateUsuarioDto
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Senha { get; set; }
}