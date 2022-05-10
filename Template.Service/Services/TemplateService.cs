using AutoMapper;
using Template.Domain.Commands;
using Template.Domain.Interfaces.Infra;
using Template.Domain.Interfaces.Service;
using Template.Domain.Models;
using Template.Domain.ViewModels;
using Template.Service.Validators.Template;

namespace Template.Service.Services
{
    public class TemplateService : BaseService, ITemplateService
    {
        private readonly IMapper _mapper;
        private readonly ITemplateRepository _TemplateRepository;

        public TemplateService
        (
            IMapper mapper,
            ITemplateRepository TemplateRepository
        )
        {
            _mapper = mapper;
            _TemplateRepository = TemplateRepository;
        }

        public async Task<IEnumerable<TemplateViewModel>> GetAll() => _mapper.Map<IEnumerable<TemplateViewModel>>(await _TemplateRepository.GetAll());

        public async Task<TemplateViewModel> GetById(int id) => _mapper.Map<TemplateViewModel>(await _TemplateRepository.GetById(id));

        public async Task<int> Insert(TemplateCommand command)
        {
            Validate(command, new InsertTemplateValidator());

            var model = _mapper.Map<_Template>(command);
            model.CriadoPor = 123; // Id do usuario logado

            return await _TemplateRepository.Insert(model);
        }

        public async Task<bool> Update(TemplateCommand command)
        {
            Validate(command, new UpdateTemplateValidator());

            var model = _mapper.Map<_Template>(command);
            model.AlteradoPor = 123; // Id do usuario logado

            return await _TemplateRepository.Update(model);

        }
        public async Task<bool> Delete(int id)
        {
            _Template model = new()
            {
                AlteradoPor = 123, // Id do usuario logado
                Id = id
            };

            return await _TemplateRepository.Delete(model);
        }
    }
}
