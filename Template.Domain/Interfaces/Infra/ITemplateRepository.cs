using Template.Domain.Models;

namespace Template.Domain.Interfaces.Infra
{
    public interface ITemplateRepository
    {
        Task<_Template> GetByName(string name);
        Task<_Template> GetById(int id);
        Task<IEnumerable<_Template>> GetAll();
        Task<int> Insert(_Template model);
        Task<bool> Update(_Template model);
        Task<bool> Delete(_Template model);
    }
}
