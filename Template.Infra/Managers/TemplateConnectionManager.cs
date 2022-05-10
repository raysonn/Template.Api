using System.Data;

namespace Template.Infra.Managers
{
    public class TemplateConnectionManager : BaseConnectionManager
    {
        public TemplateConnectionManager()
        {
            conn = new Lazy<IDbConnection>(() => ConnectionFactory.GetTemplateOpenConnection());
        }
    }
}
