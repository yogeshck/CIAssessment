using CIAssessment.Helpers;
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
            _busyLock = new object();
            _lastActivityTime = DateTime.Now;
        }
        #endregion

        #region Command Processing
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task PoppedAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public bool SystemIsIdle()
        {
            bool isIdle = true;
            lock (_busyLock)
            {
                if (IsBusy)
                    isIdle = false;
                else
                    IsBusy = true;
            }

            _lastActivityTime = DateTime.Now;
            return isIdle;
        }
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
        private object _busyLock;

        private static DateTime _lastActivityTime;
        private static bool _activityTimer = false;
        protected static bool _loginPage = false;
        #endregion
    }
}
