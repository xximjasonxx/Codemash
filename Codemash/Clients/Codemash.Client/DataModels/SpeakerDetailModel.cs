using System.Text.RegularExpressions;

namespace Codemash.Client.DataModels
{
    public class SpeakerDetailModel
    {
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

        public string Name { get; set; }

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
            set { _twitter = value; }
        }

        public string BlogUrl { get; set; }
        public string Biography { get; set; }
    }
}
