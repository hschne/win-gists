using System;

namespace WinGistsConfiguration.Configuration
{
    public class Configuration
    {
        public String Username { get; set; }

        public String Password { get; set; }

        public Boolean SaveCredentials { get; set; }

        public Boolean OpenAfterUpload { get; set; }

        public Boolean CopyUrlToClipboard { get; set; }

        public Boolean UploadAnonymously { get; set; }

        public Boolean ShowBubbleNotifications { get; set; }

        protected bool Equals(Configuration other){
            return string.Equals(Username, other.Username) && string.Equals(Password, other.Password) &&
                   SaveCredentials.Equals(other.SaveCredentials) && OpenAfterUpload.Equals(other.OpenAfterUpload) &&
                   CopyUrlToClipboard.Equals(other.CopyUrlToClipboard) &&
                   UploadAnonymously.Equals(other.UploadAnonymously) &&
                   ShowBubbleNotifications.Equals(other.ShowBubbleNotifications);
        }


        public override int GetHashCode(){
            unchecked{
                int hashCode = (Username != null ? Username.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ SaveCredentials.GetHashCode();
                hashCode = (hashCode*397) ^ OpenAfterUpload.GetHashCode();
                hashCode = (hashCode*397) ^ CopyUrlToClipboard.GetHashCode();
                hashCode = (hashCode*397) ^ UploadAnonymously.GetHashCode();
                hashCode = (hashCode*397) ^ ShowBubbleNotifications.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(Object configuration){
            if (ReferenceEquals(null, configuration)) return false;
            if (ReferenceEquals(this, configuration)) return true;
            if (configuration.GetType() != GetType()) return false;
            return Equals((Configuration) configuration);
        }
    }
}