using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WinGistsConfiguration.Configuration;
using WinGistsConfiguration.ViewModels;

namespace WinGistsConfiguration
{
    public partial class MainWindow : MetroWindow
    {
        [DllImport("user32.dll")]
        private static extern
            bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern
            bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern
            bool IsIconic(IntPtr hWnd);

        private const int SwRestore = 9;

        private readonly ConfigurationViewModel viewModel;
        private MessageDialogResult result;

        public MainWindow(){
            BringExistingToFront();
            InitializeComponent();
            viewModel = new ConfigurationViewModel();
            Closing += CloseWindow;
            DataContext = viewModel;
            viewModel.ShowDialog += ShowMessageDialog;

            if (viewModel.CloseAction == null){
                viewModel.CloseAction = Close;
            }
        }

        private void CloseWindow(Object sender, CancelEventArgs e){
            if (ConfigurationManager.ConfigurationChanged()){
                ShowMessageDialog(null, null);
                if (result == MessageDialogResult.Negative){
                    e.Cancel = true;
                }
            }
        }

        private void BringExistingToFront(){
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1)
            {
                Process p = Process.GetCurrentProcess();
                int n = 0;      
                if (processes[0].Id == p.Id)
                {
                    n = 1;
                }
                IntPtr hWnd = processes[n].MainWindowHandle;
                if (IsIconic(hWnd))
                {
                    ShowWindowAsync(hWnd, SwRestore);
                }
                SetForegroundWindow(hWnd);
                Close();
            }
        }

        private bool AlreadyOpened(){
             string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            return processes.Length > 1;
        }

        private async void ShowMessageDialog(object sender, RoutedEventArgs e){
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;

            var mySettings = new MetroDialogSettings{
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "Cancel",
                AnimateHide = false,
                ColorScheme = MetroDialogColorScheme.Theme
            };
            MessageDialogResult thisResult = await this.ShowMessageAsync("Warning", "You have unsaved changes. Are you sure you want to exit? ",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
            result = thisResult;
            if (thisResult == MessageDialogResult.Affirmative)
            {
                Close();
            }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e){
            SecureString password = PasswordBox.SecurePassword;
            viewModel.SecurePassword = password.ConvertToUnsecureString().Encrypt();
        }
    }
}