using CIAssessment.Models.CutomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.Repository
{
    interface IRepositoryService
    {
        Task<List<Node>> GetRootAssemblies();
    }
}
