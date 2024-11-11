using AutoMapper;
using fornecedor_api.Models.DTOs;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Mappings;

public class EnderecoFonecedorProfile : Profile
{
    public EnderecoFonecedorProfile()
    {
        CreateMap<CreateEnderecoFornecedorDto, EnderecoFornecedor>();
        CreateMap<EnderecoFornecedor, ReadEnderecoFornecedorDto>();
    }
    
}