using Codemash.Client.Common;
using Codemash.Client.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace Codemash.Client
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class SessionDetails : Codemash.Client.Common.LayoutAwarePage
    {
        public SessionDetails()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = (Session) navigationParameter;
            pageTitle.Text = item.Title;
            tbSpeakerName.Text = item.SpeakerName;
            tbStarts.Text = item.StartsAtDisplay;
            tbRoom.Text = item.Room;
            tbTrack.Text = item.Track;
            tbLevel.Text = item.Level;
            tbAbstract.Text = item.Abstract;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //var selectedItem = (SampleDataItem)this.flipView.SelectedItem;
            //pageState["SelectedItem"] = selectedItem.UniqueId;
        }

        private void ViewSpeaker(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SpeakerDetails), new Speaker
                                                            {
                                                                SpeakerName = "Sergei Brombvroksy",
                                                                Biography = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis vel euismod mi. Vestibulum et velit quis tortor facilisis sodales id nec lectus. Vivamus pellentesque tincidunt turpis, malesuada tempus enim malesuada eget. Sed molestie tempor mauris, non dapibus est rutrum tempor. Ut quam nisi, vestibulum et dapibus at, porta at massa. Duis vitae rutrum velit. Duis at ipsum sit amet nibh scelerisque euismod. Praesent auctor turpis in enim mollis in molestie eros pellentesque. Aenean vel nisi massa.
Pellentesque eget felis erat. Morbi gravida imperdiet ipsum, eu lacinia leo venenatis at. Nullam id enim a dui pretium ultrices. Donec condimentum varius ligula, et sagittis libero pellentesque eget. Nam viverra, elit quis accumsan ultricies, sapien nibh rutrum nisi, eget placerat nisi augue a quam. Aliquam ultricies sagittis lorem convallis consectetur. Mauris dapibus iaculis aliquet. Quisque placerat neque volutpat nulla pretium a dictum odio congue. Ut non nunc ipsum. Nullam ac leo massa, eget sollicitudin leo.
Curabitur ut adipiscing est. Proin sapien massa, commodo quis aliquet et, ultrices nec mauris. Suspendisse facilisis rhoncus nisl et ullamcorper. Integer vel urna id mi feugiat lacinia vel a lectus. Quisque rhoncus mauris dolor, eu imperdiet nisl. Integer dignissim, dui nec dapibus tempus, nisl risus vehicula quam, sit amet molestie turpis erat sagittis eros. Donec blandit, magna a faucibus molestie, lorem nisi pretium mauris, at varius erat nibh at ante. Ut luctus libero ac leo commodo id fermentum arcu vestibulum. In non metus sapien. Nullam cursus pharetra leo, sed mattis metus ultricies eu. Sed luctus elementum magna eget tristique. Cras fringilla leo ac augue imperdiet eget pretium ipsum bibendum. Nunc nibh elit, fermentum vel viverra ac, tincidunt eget ligula. Aliquam imperdiet mollis lobortis. Proin id tristique metus. Vestibulum ut nisl enim.
Vivamus faucibus fermentum ultrices. Cras eu tellus quis neque facilisis molestie. Nunc a turpis orci. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Fusce sit amet nulla odio. Suspendisse pellentesque volutpat diam, ac porttitor risus euismod ac. Duis mollis augue in tellus iaculis at ultricies metus fringilla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla rutrum, urna vitae varius rhoncus, velit libero interdum lacus, quis volutpat nisl ipsum id neque. Donec ut lorem in neque tincidunt malesuada. Cras ligula odio, venenatis eget vestibulum ut, bibendum a magna. Fusce et molestie massa. Donec bibendum pharetra fringilla. Vestibulum vitae metus vitae quam tempus ultrices. Aliquam fermentum semper augue nec pulvinar.
Nulla pharetra est at libero aliquet eget aliquet ipsum cursus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris nibh ligula, sollicitudin vitae elementum quis, placerat vel lacus. Curabitur scelerisque scelerisque consequat. Fusce augue justo, gravida eu semper nec, cursus sed erat. Maecenas nulla augue, dignissim ut sodales vel, interdum euismod mauris. Maecenas ullamcorper massa quis sem blandit lobortis. Etiam suscipit nisl id nisi consectetur lobortis tristique tellus malesuada. Vivamus adipiscing lectus at nisl tristique iaculis gravida magna semper.",
                                                                Twitter = "jfarrell"
                                                            });
        }
    }
}
