using System.Windows;
using GistClientConfiguration.ViewModels;
using MahApps.Metro.Controls;

namespace GistClientConfiguration
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(){
            InitializeComponent();
            var viewModel = new ConfigurationViewModel();
            DataContext = viewModel;
            if (viewModel.CloseAction == null){
                viewModel.CloseAction = Close;
            }
        }
    }
}