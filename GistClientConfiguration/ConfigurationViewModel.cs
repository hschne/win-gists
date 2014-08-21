using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GistClientConfiguration.Annotations;
using GistClientConfiguration.Configuration;
using GistClientConfiguration.Helper;

namespace GistClientConfiguration
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        public ConfigurationViewModel(){
            ConfigurationManager.Load();
            SaveCommand = new RelayCommand(x => Save());
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save(){
            ConfigurationManager.Save();
            Console.WriteLine(ConfigurationManager.Configuration.ToString());
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