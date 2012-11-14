using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
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

            this.UnhandledException += App_UnhandledException;
        }

        void App_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}