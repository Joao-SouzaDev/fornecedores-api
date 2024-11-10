using AutoMapper;
using fornecedor_api.Models.DTOs;
using fornecedor_api.Models.Entities;

namespace fornecedor_api.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
        CreateMap<Usuario, UsuarioAutenticadoDto>();
    }
}