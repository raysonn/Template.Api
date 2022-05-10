using AutoMapper;
using Template.Domain.Commands;
using Template.Domain.Models;

namespace Template.Domain.AutoMapper
{
    internal class CommandToDomainMappingProfile : Profile
    {
        public CommandToDomainMappingProfile()
        {
            CreateMap<TemplateCommand, _Template>();
        }
    }
}
