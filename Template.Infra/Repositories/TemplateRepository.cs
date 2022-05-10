using Dapper;
using Template.Domain.Interfaces.Infra;
using Template.Domain.Models;
using Template.Infra.Managers;

namespace Template.Infra.Repositories
{
    public partial class TemplateRepository : BaseRepository, ITemplateRepository
    {
        public TemplateRepository(TemplateConnectionManager connectionManager) : base(connectionManager) { }

        public async Task<_Template> GetByName(string name) => await _conn.QueryFirstOrDefaultAsync<_Template>(_GetByName, new { name });

        public async Task<IEnumerable<_Template>> GetAll() => await _conn.QueryAsync<_Template>(_GetAll);

        public async Task<_Template> GetById(int id) => await _conn.QueryFirstOrDefaultAsync<_Template>(_GetById, new { id });

        public async Task<int> Insert(_Template model) => await _conn.ExecuteScalarAsync<int>(_Insert, model);

        public async Task<bool> Update(_Template model) => await _conn.ExecuteAsync(_Update, model) > 0;

        public async Task<bool> Delete(_Template model) => await _conn.ExecuteAsync(_Delete, model) > 0;
    }
}
