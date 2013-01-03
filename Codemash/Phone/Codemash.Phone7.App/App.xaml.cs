using System.Windows;
using Codemash.Phone.Shared;

namespace Codemash.Phone7.App
{
    public partial class App : Application
    {
        public App()
        {
            // Standard Silverlight initialization
            InitializeComponent();

            this.UnhandledException += App_UnhandledException;
        }

        void App_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject.GetType() == typeof (NoInternetConnectionException))
                MessageBox.Show("Could not find an Internet connection - please try again later");
            else
                MessageBox.Show("An error occured - the application will now close");
        }
    }
}