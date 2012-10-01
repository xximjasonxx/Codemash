using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data
{
    public class SessionDataSource
    {
        private static SessionDataSource _sampleDataSource = new SessionDataSource();

        public static IEnumerable<SessionGroup> GetGroups()
        {
            return _sampleDataSource.AllGroups;
        }

        public IEnumerable<SessionGroup> AllGroups
        {
            get
            {
                var returnList = new List<SessionGroup>();
                var group1 = new SessionGroup("Favorites");
                group1.Sessions.Add(new Session
                                        {
                                            Title = "SQL Server Internals",
                                            SpeakerName = "Jason Farrell",
                                            Room = "Conv. Ctr. G (Pearson)",
                                            StartsAt = DateTime.Now,
                                            Track = "Database Platforms and Development",
                                            Level = "Advanced"
                                        });
                group1.Sessions.Add(new Session
                                        {
                                            Title = "Using HTML5 to Build Mobile Apps",
                                            SpeakerName = "Todd Anglin",
                                            Room = "Conv. Ctr. 3 (CTS)",
                                            StartsAt = DateTime.Now,
                                            Track = "Mobile Development",
                                            Level = "Intermediate"
                                        });
                group1.Sessions.Add(new Session
                                        {
                                            Title = "What's New in Windows Azure",
                                            SpeakerName = "Michael Collier",
                                            Room = "Conv. Ctr. 12 (Orasi)",
                                            StartsAt = DateTime.Now,
                                            Track = "Cloud",
                                            Level = "Beginner"
                                        });
                returnList.Add(group1);

                var group2 = new SessionGroup("Upcoming");
                group2.Sessions.Add(new Session
                                        {
                                            Title = "WiFu - so you think your wireless connection is safe?",
                                            SpeakerName = "Rob Gillen",
                                            Room = "Conv. Ctr. 5 (Wintellect)",
                                            StartsAt = DateTime.Now,
                                            Track = "Open",
                                            Level = "General"
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "A Sneak Peek of Visual Studio 11",
                                            SpeakerName = "Randy Pagels",
                                            Room = "Conv. Ctr. E (Telerik)",
                                            StartsAt = DateTime.Now,
                                            Track = "Developer Tools, Languages, and Frameworks",
                                            Level = "Beginner"
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "Advanced SharePoint Web Part Development",
                                            SpeakerName = "Rob Windsor",
                                            Room = "Conv. Ctr. 6 (PubNub)",
                                            StartsAt = DateTime.Now,
                                            Track = "Open",
                                            Level = "Intermediate"
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "Finding your Customer DNA",
                                            SpeakerName = "Michael Neel",
                                            Room = "Conv. Ctr. 3 (CTS)  ",
                                            StartsAt = DateTime.Now,
                                            Track = "Database Platforms and Development",
                                            Level = "Intermediate"
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "How to Ride the Service Bus with Azure",
                                            SpeakerName = "Andy Leonard",
                                            Room = "Conv. Ctr. 12 (Orasi)",
                                            StartsAt = DateTime.Now,
                                            Track = "Database Platforms and Development",
                                            Level = "Intermediate"
                                        });
                returnList.Add(group2);

                return returnList;
            }
        }
    }
}
