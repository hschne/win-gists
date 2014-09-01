using System;
using System.Drawing;
using System.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Uploader.Properties;

namespace Uploader
{
    public class UploaderIcon : IDisposable
    {
        private readonly Icon icon = Resources.TaskbarIcon;

        public UploaderIcon(){
            TaskbarIcon = new TaskbarIcon{Icon = icon,};
            FadeTime = 3000;
        }

        private TaskbarIcon TaskbarIcon { get; set; }

        public int FadeTime { get; set; }

        public void Dispose(){
            TaskbarIcon.Dispose();
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
    }
}