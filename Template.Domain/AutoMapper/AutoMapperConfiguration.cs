using AutoMapper;

namespace Template.Domain.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static IMapperConfigurationExpression RegisterMappings(IMapperConfigurationExpression conf)
        {
            conf.AddProfile(new DomainToViewModelMappingProfile());
            conf.AddProfile(new ViewModelToDomainMappingProfile());
            conf.AddProfile(new CommandToDomainMappingProfile());
            return conf;
        }
    }
}
