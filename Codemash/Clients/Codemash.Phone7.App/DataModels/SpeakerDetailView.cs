using System.Text.RegularExpressions;
using Codemash.Phone7.Data.Entities;

namespace Codemash.Phone7.App.DataModels
{
    public class SpeakerDetailView
    {
        public string Biography { get; private set; }
        public string Name { get; private set; }
        public string BlogUrl { get; private set; }

        public string PictureUrl
        {
            get
            {
                if (string.IsNullOrEmpty(Twitter))
                    return string.Empty;

                Regex regex = new Regex(@"^@");
                string twitterUser = regex.Replace(Twitter, string.Empty);
                return string.Format("https://api.twitter.com/1/users/profile_image?screen_name={0}&size=original", twitterUser);
            }
        }

        private string _twitter;
        public string Twitter
        {
            get
            {
                if (string.IsNullOrEmpty(_twitter))
                    return "No Twitter";

                if (!_twitter.StartsWith("@"))
                    _twitter = "@" + _twitter;
                return _twitter;
            }
            private set { _twitter = value; }
        }

        public SpeakerDetailView(Speaker speaker)
        {
            Biography = speaker.Biography;
            BlogUrl = speaker.BlogUrl;
            Name = speaker.Name;
            Twitter = speaker.Twitter;
        }
    }
}
