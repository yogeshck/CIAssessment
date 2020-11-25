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

        // Command to Export Json
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
            try
            {
                ClearMessage();
                TabSource = new ObservableCollection<TabItem>(RepositoryService.GetRootAssemblies());
            }catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region Processing Methods

        private void SetPartNumberVisible(bool isPart)
        {
            try
            {
                ClearMessage();
                foreach (var tabItem in TabSource)
                {
                    RepositoryService.UpdateTabItemVisibilty(tabItem.Content, isPart, !isPart);
                }
                UpdateSelectedIndexTab();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private void SetFileNameVisible(bool isFile)
        {
            try
            {
                ClearMessage();
                foreach (var tabItem in TabSource)
                {
                    RepositoryService.UpdateTabItemVisibilty(tabItem.Content, !isFile, isFile);
                }
                UpdateSelectedIndexTab();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        private void UpdateSelectedIndexTab()
        {
            ClearMessage();
            var temp = SelectedTab;
            SelectedTab = null;
            SelectedTab = temp;
        }

        private void ExportJsonTask(object obj)
        {
            try
            {
                ClearMessage();
                var msg = ConvertEntities.GetJsonForNode(SelectedTab.Content);
                SuccessMessage = $"Exported to location {msg}";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private void ClearMessage()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
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

        public string ErrorMessage
        {
            get { return _error; }
            set { _error = value; RaisePropertyChanged(() => ErrorMessage);
                RaisePropertyChanged(() => IsErrorMessage);
            }
        }

        public string SuccessMessage
        {
            get { return _success; }
            set
            {
                _success = value; RaisePropertyChanged(() => SuccessMessage);
                RaisePropertyChanged(() => IsSuccessMessage);
            }
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
            set { _selectedTab = value; RaisePropertyChanged(() => SelectedTab); ClearMessage(); }
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

        public Visibility IsErrorMessage
        {
            get { return !string.IsNullOrEmpty(_error) ? Visibility.Visible: Visibility.Hidden; }
        }

        public Visibility IsSuccessMessage
        {
            get { return !string.IsNullOrEmpty(_success) ? Visibility.Visible : Visibility.Hidden; }
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
        private string _error;
        private string _success;
        #endregion
    }
}
