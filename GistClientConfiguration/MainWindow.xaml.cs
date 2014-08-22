using System.Windows;
using GistClientConfiguration.ViewModels;

namespace GistClientConfiguration
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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