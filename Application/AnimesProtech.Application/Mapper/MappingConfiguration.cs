using AnimesProtech.Application.Mapper.Mappings;
using AutoMapper;

namespace AnimesProtech.Application.Mapper;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile(new DomainToViewModelMappingProfile());
            config.AddProfile(new ViewModelToViewModelMappingProfile());
        });
    }
}