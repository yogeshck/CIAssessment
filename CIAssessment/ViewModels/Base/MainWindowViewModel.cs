using CIAssessment.Models.CutomModel;
using CIAssessment.ViewModels.Helper;
using CIAssessment.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace CIAssessment.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        #region Command
        public ICommand ExportJsonCommand => new RelayCommand(ExportJsonTask);
        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            ApplyText();
            Initialize();
            _isPart = true;
            _isFile = false;
        }

        void ApplyText()
        {
            _fileRadioTxt = "File Names";
            _partRadioTxt = "Part Numbers";
        }

        #endregion

        #region Initialize
        public void Initialize()
        {
            TabSource = new ObservableCollection<TabItem>(RepositoryService.GetRootAssemblies());
        }

        #endregion

        #region Processing Methods

        private void SetPartNumberVisible(bool isPart)
        {
            foreach(var tabItem in TabSource)
            {
                RepositoryService.UpdateTabItemVisibilty(tabItem.Content, isPart, !isPart);
            }
            UpdateSelectedIndexTab();
        }

        private void SetFileNameVisible(bool isFile)
        {
            foreach (var tabItem in TabSource)
            {
                RepositoryService.UpdateTabItemVisibilty(tabItem.Content, !isFile, isFile);
            }
            UpdateSelectedIndexTab();
        }

        private void UpdateSelectedIndexTab()
        {
            var temp = SelectedTab;
            SelectedTab = null;
            SelectedTab = temp;
        }

        private void ExportJsonTask(object obj)
        {
            ConvertEntities.GetJsonForNode(SelectedTab.Content);
        }
        #endregion

        #region Properties

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

        public Node SelectedItem
        {
            get { return _selected; }
            set { _selected = value; RaisePropertyChanged(() => SelectedItem); }
        }

        public TabItem SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; RaisePropertyChanged(() => SelectedTab); }
        }

        public ImageSource Image
        {
            get
            {
                if (SelectedItem == null)
                    return null;

                BitmapImage image = new BitmapImage();
                using(var mem = new MemoryStream(SelectedItem.Preview))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
        }

        public bool IsPartVisible
        {
            get { return _isPart; }
            set 
            { 
                _isPart = value;
                SetPartNumberVisible(_isPart);
                RaisePropertyChanged(() => IsPartVisible); 
            }
        }

        public bool IsFileVisible
        {
            get { return _isFile; }
            set 
            { 
                _isFile = value; 
                SetFileNameVisible(_isFile); 
                RaisePropertyChanged(() => IsFileVisible); 
            }
        }

        #endregion

        #region Members
        private ObservableCollection<TabItem> _tabSource;
        private TabItem _selectedTab;
        private string _partRadioTxt;
        private string _fileRadioTxt;

        private Node _selected;
        private bool _isPart;
        private bool _isFile;
        #endregion
    }
}
