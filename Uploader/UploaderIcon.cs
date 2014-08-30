using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;

namespace Uploader
{
    public class UploaderIcon : IDisposable
    {
        private readonly Icon icon = Properties.Resources.TaskbarIcon;

        private TaskbarIcon TaskbarIcon { get; set; }

        public UploaderIcon(){
            TaskbarIcon = new TaskbarIcon { Icon = icon, ToolTipText = "Hello, lel!" };
        }

        public void ShowStandardBaloon(String message){
            string title = "WinGists";
            TaskbarIcon.ShowBalloonTip(title, message, BalloonIcon.None);
        }

        public void ShowErrorBallon(String message){
            string title = "Error";
            TaskbarIcon.ShowBalloonTip(title, message, BalloonIcon.Error);
        }

        public void Dispose(){
            TaskbarIcon.Dispose();
        }
    }
}
