using PeopleManager.WcfService;
using System.ServiceModel;
using System.Windows;

namespace PeopleManager.WinClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            FaultException<PersonIsInvalidFault> ex = e.Exception as FaultException<PersonIsInvalidFault>;
            if (ex != null)
            {
                MessageBox.Show(ex.Detail.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }
    }
}