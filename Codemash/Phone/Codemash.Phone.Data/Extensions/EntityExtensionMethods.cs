using System;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Extensions
{
    public static class EntityExtensionMethods
    {
        // speaker property names
        private const string BiographyPropertyName = "Biography";
        private const string BlogUrlPropertyName = "BlogUrl";
        private const string EmailAddressPropertyName = "EmailAddress";
        private const string NamePropertyName = "Name";
        private const string TwitterPropertyName = "Twitter";

        /// <summary>
        /// Update the property on the extended object
        /// </summary>
        /// <param name="speaker">The speaker instance being extended</param>
        /// <param name="keyName">The name of the property</param>
        /// <param name="value">The new value to be used</param>
        internal static void UpdateProperty(this Speaker speaker, string keyName, string value)
        {
            switch (keyName)
            {
                case BiographyPropertyName:
                    speaker.Biography = value;
                    break;
                case BlogUrlPropertyName:
                    speaker.BlogUrl = value;
                    break;
                case NamePropertyName:
                    speaker.Name = value;
                    break;
                case TwitterPropertyName:
                    speaker.Twitter = value;
                    break;
                default:
                    throw new InvalidOperationException("Invalid Key Name for Update specified for Speaker");
            }
        }

        private const string AbstractPropertyName = "Abstract";
        private const string EndPropertyName = "End";
        private const string LevelPropertyName = "Level";
        private const string RoomPropertyName = "Room";
        private const string SessionSpeakerIdPropertyName = "SpeakerId";
        private const string StartPropertyName = "Start";
        private const string TitlePropertyName = "Title";
        private const string TrackPropertyName = "Track";

        /// <summary>
        /// Update a property by name for a session instance
        /// </summary>
        /// <param name="session">The session instance being updated</param>
        /// <param name="keyName">The name of the property to be changed</param>
        /// <param name="value">The new value for the property</param>
        internal static void UpdateProperty(this Session session, string keyName, string value)
        {
            switch (keyName)
            {
                case AbstractPropertyName:
                    session.Abstract = value;
                    break;
                case EndPropertyName:
                    session.Ends = value;
                    break;
                case LevelPropertyName:
                    session.Difficulty = value;
                    break;
                case RoomPropertyName:
                    session.Room = value;
                    break;
                case SessionSpeakerIdPropertyName:
                    session.SpeakerId = value.AsLong();
                    break;
                case StartPropertyName:
                    session.Starts = value;
                    break;
                case TitlePropertyName:
                    session.Title = value;
                    break;
                case TrackPropertyName:
                    session.Technology = value;
                    break;
                default:
                    throw new InvalidOperationException("Invalid Key Name provided for Session Updated");
            }
        }
    }
}
