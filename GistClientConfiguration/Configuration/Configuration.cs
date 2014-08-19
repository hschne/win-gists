using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GistClientConfiguration.Configuration {
    public class Configuration {

        public String Username { get; set; }

        public String Password { get; set; }

        public Boolean SaveCredentials { get; set; }

        public Boolean OpenAfterUpload { get; set; }

        public Boolean CopyUrlToClipboard { get; set; }

        public Boolean UploadAnonymously { get; set; }

     }
}
