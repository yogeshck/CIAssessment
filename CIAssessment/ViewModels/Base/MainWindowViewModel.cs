using CIAssessment.Models.CutomModel;
using CIAssessment.ViewModels.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIAssessment.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ApplyText();
        }

        void ApplyText()
        {
            _fileRadioTxt = "File Names";
            _partRadioTxt = "Part Numbers";
        }

        public override Task InitializeAsync(object navigationData)
        {
            TabSource = new ObservableCollection<TabItem>(RepositoryService.GetRootAssemblies());
            return base.InitializeAsync(navigationData);
        }

        public string FileRadioLbl
        {
            get { return _fileRadioTxt; }
            set { _fileRadioTxt = value; RaisePropertyChanged(() => FileRadioLbl); }
        }

        public string PartRadioLbl
        {
            get { return _partRadioTxt; }
            set { _partRadioTxt = value; RaisePropertyChanged(() => PartRadioLbl); }
        }

        public ObservableCollection<TabItem> TabSource
        {
            get { return _tabSource; }
            set { _tabSource = value; RaisePropertyChanged(() => TabSource); }
        }

        private ObservableCollection<TabItem> _tabSource;
        private string _partRadioTxt;
        private string _fileRadioTxt;
    }
}
