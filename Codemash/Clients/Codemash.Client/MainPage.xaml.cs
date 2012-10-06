using System.Collections.Generic;
using Codemash.Client.Classes;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Codemash.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var sessionList = new List<Session>();
            sessionList.Add(new Session
                                {
                                    Title = "This is a Test Sesion with a fairly long title",
                                    Room = "Indigo",
                                    SpeakerName = "Greg Levenhagen",
                                    Time = "1/12 2013 8:00am",
                                    Track = "Ruby"
                                });
            sessionList.Add(new Session
                                {
                                    Title = "This is a Test Sesion with a fairly long title",
                                    Room = "Indigo",
                                    SpeakerName = "Greg Levenhagen",
                                    Time = "1/12 2013 8:00am",
                                    Track = "Ruby"
                                });
            sessionList.Add(new Session
                                {
                                    Title = "This is a Test Sesion with a fairly long title",
                                    Room = "Indigo",
                                    SpeakerName = "Greg Levenhagen",
                                    Time = "1/12 2013 8:00am",
                                    Track = "Ruby"
                                });

            theGridView.ItemsSource = sessionList;
        }

        private void theGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var session = (Session) e.ClickedItem;
            
        }
    }
}
