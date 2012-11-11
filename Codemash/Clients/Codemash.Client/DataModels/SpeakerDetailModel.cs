using System.Text.RegularExpressions;

namespace Codemash.Client.DataModels
{
    public class SpeakerDetailModel
    {
        public string PictureUrl
        {
            get
            {
                if (!HasTwitter)
                    return "/Assets/NoImage.jpg";

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
                if (NotHasTwitter)
                    return string.Empty;

                if (!_twitter.StartsWith("@"))
                    _twitter = "@" + _twitter;
                return _twitter;
            }
            set
            {
                _twitter = value;
                HasTwitter = !string.IsNullOrEmpty(value);
            }
        }

        public bool HasTwitter { get; private set; }
        public bool NotHasTwitter { get { return !HasTwitter; } }

        private string _blogUrl;
        public string BlogUrl
        {
            get
            {
                return NotHasBlog ? string.Empty : _blogUrl;
            }
            set
            {
                _blogUrl = value;
                HasBlog = !string.IsNullOrEmpty(value);
            }
        }

        public bool HasBlog { get; private set; }
        public bool NotHasBlog { get { return !HasBlog; } }

        public string Biography { get; set; }
    }
}
