using CIAssessment.Models;
using CIAssessment.Models.CutomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private DataEntities dataContext;

        public RepositoryService(DataEntities datacontext = null)
        {
            if (datacontext == null)
                dataContext = new DataEntities();
            else
                dataContext = datacontext;
        }

        public List<TabItem> GetRootAssemblies()
        {
            var items = (from c in dataContext.Configurations
                         where c.IsRoot.HasValue && c.IsRoot.Value && !string.IsNullOrEmpty(c.ConfigurationName)
                         orderby c.Id descending
                         select new TabItem { Header = c.ConfigurationName }).ToList();
            return items;
        }
    }
}
