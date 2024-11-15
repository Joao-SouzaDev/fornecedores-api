﻿using System.ComponentModel.DataAnnotations;

namespace fornecedor_api.Models.DTOs;

public class CreateFornecedorDto
{
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Telefone { get; set; }
}