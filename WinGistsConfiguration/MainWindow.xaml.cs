using System;
using System.ComponentModel;
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
        private readonly ConfigurationViewModel viewModel;
        private MessageDialogResult result;

        public MainWindow(){
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