using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using GistClientConfiguration.Configuration;
using GistClientConfiguration.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GistClientConfiguration
{
    public partial class MainWindow : MetroWindow
    {

        private ConfigurationViewModel viewModel;

        public MainWindow(){
            InitializeComponent();
            viewModel = new ConfigurationViewModel();
            DataContext = viewModel;
            viewModel.ShowDialog += ShowMessageDialog;
            
            if (viewModel.CloseAction == null){
                viewModel.CloseAction = Close;
            }
        }

        private async void ShowMessageDialog(object sender, RoutedEventArgs e)
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "Cancel",
                AnimateHide = false,
                ColorScheme = MetroDialogColorScheme.Theme
            };
            MessageDialogResult result = await this.ShowMessageAsync("Warning", "You have unsaved changes. Are you sure you want to exit? ",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (result == MessageDialogResult.Affirmative){
                Close();
            }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e){
            SecureString password = PasswordBox.SecurePassword;
            viewModel.SecurePassword = password.ConvertToUnsecureString().Encrypt();
        }
    }
}