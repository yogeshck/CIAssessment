using CIAssessment.Helpers;
using CIAssessment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.ViewModels.Helper
{
    public class ViewModelBase : PropertyChangeHelper
    {
        #region Constructor
        public ViewModelBase()
        {
            RepositoryService = new RepositoryService();
        }
        #endregion

        #region Command Processing
        #endregion

        #region Properties
        public bool IsBusy
        {
            get { return _isBusy;}
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy);}
        }
        #endregion

        #region Private Members
        private bool _isBusy;
        protected readonly IRepositoryService RepositoryService;
        #endregion
    }
}
