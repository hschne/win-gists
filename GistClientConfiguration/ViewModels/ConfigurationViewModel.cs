using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
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
            get { return ConfigurationManager.Password; }
            set { ConfigurationManager.Password = value; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save(){
            ConfigurationManager.Save();
            CloseAction();
        }

        public void Cancel(){
            if (ConfigurationManager.ConfigurationChanged()){
                DialogResult result = MessageBox.Show("Changes have not been saved. Are you sure you want to exit?",
                    "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes){
                    CloseAction();
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