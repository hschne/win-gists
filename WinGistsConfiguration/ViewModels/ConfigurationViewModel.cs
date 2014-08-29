using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GistClientConfiguration.Annotations;
using WinGistsConfiguration.Configuration;
using WinGistsConfiguration.Helper;

namespace WinGistsConfiguration.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        public delegate void ShowDialogHandler(object obj, RoutedEventArgs e);

        public ConfigurationViewModel(){
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        public Visibility CredentialsVisible{
            get { return UploadAnonymously ? Visibility.Collapsed : Visibility.Visible; }
        }

        public String UserName{
            get { return ConfigurationManager.Username; }
            set { ConfigurationManager.Username = value; }
        }

        public String SecurePassword{
            set { ConfigurationManager.EncryptedPassword = value; }
        }

        public Boolean OpenAfterUpload{
            get { return ConfigurationManager.OpenAfterUpload; }
            set { ConfigurationManager.OpenAfterUpload = value; }
        }

        public Boolean CopyUrlToClipboard{
            get { return ConfigurationManager.CopyUrlToClipboard; }
            set { ConfigurationManager.CopyUrlToClipboard = value; }
        }

        public Boolean UploadAnonymously{
            get { return ConfigurationManager.UploadAnonymously; }
            set{
                ConfigurationManager.UploadAnonymously = value;
                OnPropertyChanged("CredentialsVisible");
            }
        }

        public Boolean ShowBubbleNotifications{
            get { return ConfigurationManager.ShowBubbleNotifications; }
            set { ConfigurationManager.ShowBubbleNotifications = value; }
        }


        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event ShowDialogHandler ShowDialog;

        public void Save(){
            ConfigurationManager.Save();
            CloseAction();
        }

        public void Cancel(){
            if (ConfigurationManager.ConfigurationChanged()){
                if (ShowDialog != null){
                    ShowDialog(this, null);
                }
            }
            else{
                CloseAction();
            }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null){
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}