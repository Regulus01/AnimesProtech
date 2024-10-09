using AnimesProtech.Application.ValueObjects.ViewModels;
using AnimesProtech.Domain.Entities;
using AutoMapper;

namespace AnimesProtech.Application.Mapper.Mappings;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Anime, CriarAnimeViewModel>();
        CreateMap<Anime, EditarAnimeViewModel>();
        CreateMap<Anime, ObterAnimeViewModel>();
    }
}