using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Codemash.Phone.Shared;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Codemash.Phone8.App
{
    public partial class App : Application
    {
        public App()
        {
            // Standard Silverlight initialization
            InitializeComponent();

            UnhandledException += App_UnhandledException;
        }

        void App_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject.GetType() == typeof(NoInternetConnectionException))
                MessageBox.Show("Could not find an Internet connection - please try again later");
            else
                MessageBox.Show("An error occured - the application will now close");
        }
    }
}