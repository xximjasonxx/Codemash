using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Codemash.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionsListView : LayoutAwarePage
    {
        public SessionsListView()
        {
            this.InitializeComponent();
        }

        /*private void LoadExampleData()
        {
            theGridView.ItemsSource = new List<IListItem>
                                          {
                                              new SessionGroup { Title = "A" },
                                              new Session {Title = "A Session 1"},
                                              new Session {Title = "A Session 2"},
                                              new Session {Title = "A Session 3"},
                                              new Session {Title = "A Session 4"},
                                              new Session {Title = "A Session 5"},
                                              new Session {Title = "A Session 6"},
                                              new Session {Title = "A Session 7"},
                                              new Session {Title = "A Session 8"},
                                              new SessionGroup { Title = "B" },
                                              new Session {Title = "B Session 1"},
                                              new Session {Title = "B Session 2"},
                                              new Session {Title = "B Session 3"},
                                              new Session {Title = "B Session 4"},
                                              new SessionGroup { Title = "C" },
                                              new Session {Title = "C Session 1"},
                                              new Session {Title = "C Session 2"},
                                              new Session {Title = "C Session 3"},
                                              new Session {Title = "C Session 4"},
                                              new Session {Title = "C Session 5"},
                                              new Session {Title = "C Session 6"},
                                              new Session {Title = "C Session 7"},
                                              new Session {Title = "C Session 8"},
                                              new Session {Title = "C Session 9"},
                                              new Session {Title = "C Session 10"},
                                              new Session {Title = "C Session 11"},
                                              new Session {Title = "C Session 12"},
                                              new SessionGroup { Title = "D" },
                                              new Session {Title = "D Session 1"},
                                              new Session {Title = "D Session 2"}
                                          };
        }*/

        private void SessionItem_Click(object sender, ItemClickEventArgs e)
        {
            var session = e.ClickedItem as Session;
            if (session != null)
            {
                Frame.Navigate(typeof (SessionView), session);
            }
        }
    }
}
