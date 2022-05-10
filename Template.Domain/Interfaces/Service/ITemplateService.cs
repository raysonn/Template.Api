using Template.Domain.Commands;
using Template.Domain.ViewModels;

namespace Template.Domain.Interfaces.Service
{
    public interface ITemplateService
    {
        Task<IEnumerable<TemplateViewModel>> GetAll();
        Task<TemplateViewModel> GetById(int id);
        Task<int> Insert(TemplateCommand command);
        Task<bool> Update(TemplateCommand command);
        Task<bool> Delete(int id);
    }
}
