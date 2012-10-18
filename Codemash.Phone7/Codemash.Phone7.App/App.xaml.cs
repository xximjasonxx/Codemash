using System.Windows;

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
            throw new System.NotImplementedException();
        }
    }
}