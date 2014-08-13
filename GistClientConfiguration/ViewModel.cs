using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using GistClient.FileSystem;

using GistClientConfiguration.Annotations;

namespace GistClientConfiguration {
    public class ViewModel : INotifyPropertyChanged 
    {

        public bool SaveCredentials {
            get {
                return SettingsManager.SaveCredentials;
            }
            set {
                SettingsManager.SaveCredentials = true;
                this.OnPropertyChanged();
            }
        }

        public bool CopyUrlToClipboard {
            get {
                return SettingsManager.CopyUrlToClipboard;
            }
            set {
                SettingsManager.CopyUrlToClipboard = value;
                this.OnPropertyChanged();
            }
        }

        public bool UploadAnonymously {
            get {
                return SettingsManager.UploadAnonymously;
            }
            set {
                SettingsManager.UploadAnonymously = value;
                this.OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null ) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
