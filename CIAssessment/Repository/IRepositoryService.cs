using CIAssessment.Models.CutomModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CIAssessment.Repository
{
    public interface IRepositoryService
    {
        List<TabItem> GetRootAssemblies(bool isPart=true);

        void UpdateTabItemVisibilty(Node node,bool isPart=true,bool isFile= false);
    }
}
