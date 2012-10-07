﻿using System.Collections.Generic;
using Codemash.Client.Classes;
using Codemash.Client.Core;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Codemash.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionView : LayoutAwarePage
    {
        public SessionView()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            DataContext = (Session) navigationParameter;
        }

        private void ViewSpeaker(object sender, RoutedEventArgs e)
        {
            var session = (Session) DataContext;
            var speaker = session.Speaker;

            Frame.Navigate(typeof (SpeakerView), speaker);
        }
    }
}
