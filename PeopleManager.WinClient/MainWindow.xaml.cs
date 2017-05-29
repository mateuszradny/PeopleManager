using Microsoft.Practices.Unity;
using PeopleManager.WinClient.ViewModels;
using System.Windows;

namespace PeopleManager.WinClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = UnityConfig.GetConfiguredContainer().Resolve<PeopleViewModel>();
        }
    }
}