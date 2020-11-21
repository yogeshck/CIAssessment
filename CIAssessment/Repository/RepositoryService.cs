using CIAssessment.Models;
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


    }
}
