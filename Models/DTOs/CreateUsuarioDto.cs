using System.ComponentModel.DataAnnotations;

namespace fornecedor_api.Models.DTOs;

public class CreateUsuarioDto
{
    [Required]
    [MinLength(6, ErrorMessage = "O login deve ter mais que 6 caracteres")]
    public string Login { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "A senha deve ter mais que 8 caracteres")]
    public string Senha { get; set; }
}