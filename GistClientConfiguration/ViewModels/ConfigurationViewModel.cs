using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using GistClientConfiguration.Annotations;
using GistClientConfiguration.Configuration;
using GistClientConfiguration.Helper;
using MessageBox = System.Windows.Forms.MessageBox;

namespace GistClientConfiguration.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        public ConfigurationViewModel(){
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        public String UserName{
            get { return ConfigurationManager.Username; }
            set { ConfigurationManager.Username = value; }
        }

        public String Password{
            get{
                return Regex.Replace(ConfigurationManager.Password.Decrypt(), ".", "x"); ;
            }
            set { ConfigurationManager.Password = value.Encrypt(); }
        }

        public Boolean SaveCredentials{
            get { return ConfigurationManager.SaveCredentials; }
            set { ConfigurationManager.SaveCredentials = value; }
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
            set { ConfigurationManager.UploadAnonymously = value; }
        }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public Action CloseAction { get; set; }

        public delegate void ShowDialogHandler(object obj, RoutedEventArgs e);

        public event ShowDialogHandler ShowDialog;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save(){
            ConfigurationManager.Save();
            CloseAction();
        }

        public void Cancel(){
            if (ConfigurationManager.ConfigurationChanged()){
                if (ShowDialog!= null){
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