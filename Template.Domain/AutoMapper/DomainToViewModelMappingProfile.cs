using AutoMapper;
using Template.Domain.Models;
using Template.Domain.ViewModels;

namespace Template.Domain.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<_Template, TemplateViewModel>();
        }
    }
}
