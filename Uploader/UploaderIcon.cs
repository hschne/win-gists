using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;
using WinGistsConfiguration.Configuration;

namespace Uploader
{
    public class UploaderIcon : IDisposable
    {
        private readonly Icon icon = Properties.Resources.TaskbarIcon;

        private TaskbarIcon TaskbarIcon { get; set; }

        public int FadeTime { get; set; }

        public UploaderIcon(){
            TaskbarIcon = new TaskbarIcon { Icon = icon, };
            FadeTime = 3000;
        }

        public void ShowStandardBaloon(String message){
            string title = "WinGists";
            TaskbarIcon.ShowBalloonTip(title, message, BalloonIcon.None);
            Thread.Sleep(FadeTime);
            TaskbarIcon.HideBalloonTip();
        }

        public void ShowErrorBallon(String message){
            string title = "Error";
            TaskbarIcon.ShowBalloonTip(title, message, BalloonIcon.Error);
            Thread.Sleep(FadeTime);
            TaskbarIcon.HideBalloonTip();
        }

        public void Dispose(){
            TaskbarIcon.Dispose();
        }
    }
}
