using System;
using System.Windows;
using GistClientConfiguration.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace GistClientConfiguration
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(){
            InitializeComponent();
            var viewModel = new ConfigurationViewModel();
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

    }
}