using AutoMapper;
using fornecedor_api.Models.DTOs;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Mappings;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<CreateFornecedorDto, Fornecedor>();
        CreateMap<Fornecedor, ReadFornecedorDto>()
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));;
        CreateMap<UpdateFornecedorDto, Fornecedor>();
    }
}