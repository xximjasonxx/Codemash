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
                                            Level = "Advanced",
                                            Abstract = "There's a lot of information about SQL Server's inner workings. Much of the material is highly focused and highly specialized. Before diving deep into the secrets of a particular feature, it's important to understand how all of the pieces of SQL Server work together. During this session we'll journey from the query parser to the storage engine and talk about how the different parts of SQL Server interact. Along the way we'll will cover theoretical and practical knowledge about databases and SQL Server in specific. Although there is a lot of theoretical and esoteric knowledge available, this presentation will focus on the practical and immediate Ã¢â‚¬â€œ wherever possible knowledge of SQL Server's internals will be related improving to real world performance."
                                        });
                group1.Sessions.Add(new Session
                                        {
                                            Title = "Using HTML5 to Build Mobile Apps",
                                            SpeakerName = "Todd Anglin",
                                            Room = "Conv. Ctr. 3 (CTS)",
                                            StartsAt = DateTime.Now,
                                            Track = "Mobile Development",
                                            Level = "Intermediate",
                                            Abstract = "Native apps are great, but if you want your app to reach as many people as possible, HTML5 is your ticket. In this session, we'll explore the different ways HTML5 can be used to build and deploy mobile apps, as well as the tools that can make the job easier. If mobile is in your future, and learning Objective-C is not your idea of a good time, don't miss this session!"
                                        });
                group1.Sessions.Add(new Session
                                        {
                                            Title = "What's New in Windows Azure",
                                            SpeakerName = "Michael Collier",
                                            Room = "Conv. Ctr. 12 (Orasi)",
                                            StartsAt = DateTime.Now,
                                            Track = "Cloud",
                                            Level = "Beginner",
                                            Abstract = "Technology providers move at \"cloud speed\". Cloud computing platforms such as Windows Azure are updated more frequently than other technology platforms. The rapid pace of innovation makes it difficult to understand what features are available, and how to best utilize them in our applications. The positive aspect to the rapid updates is being able to quickly take advantage of new features and innovations. The Windows Azure platform is constantly growing and evolving. In this session we will take a quick look back at major milestones in Windows Azure's relatively brief history, and then proceed into reviewing recent platform updates and new features now available. Coming away you will have a solid understanding of Windows Azure platform features available for you to use in your applications today."
                                        });
                returnList.Add(group1);

                var allSessionsGroup = new SessionGroup("All Sessions");
                allSessionsGroup.Sessions.Add(new Session
                                                  {
                                                      Title = "By Block"
                                                  });

                allSessionsGroup.Sessions.Add(new Session
                                                  {
                                                      Title = "By Track"
                                                  });
                returnList.Add(allSessionsGroup);

                var group2 = new SessionGroup("Upcoming");
                group2.Sessions.Add(new Session
                                        {
                                            Title = "WiFu - so you think your wireless connection is safe?",
                                            SpeakerName = "Rob Gillen",
                                            Room = "Conv. Ctr. 5 (Wintellect)",
                                            StartsAt = DateTime.Now,
                                            Track = "Open",
                                            Level = "General",
                                            Abstract = "In this session we'll discuss various wireless security techniques including common misconceptions and mis-configurations. We will demonstrate how easy it is to compromise even \"secured\" connections and what the implications are for you as an IT professional. Using free software and inexpensive hardware (~$30), we'll demonstrate a number of attacks and highlight the vulnerabilities that are present in the behavior of many wireless devices."
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "A Sneak Peek of Visual Studio 11",
                                            SpeakerName = "Randy Pagels",
                                            Room = "Conv. Ctr. E (Telerik)",
                                            StartsAt = DateTime.Now,
                                            Track = "Developer Tools, Languages, and Frameworks",
                                            Level = "Beginner",
                                            Abstract = "Microsoft's application lifecycle management tooling is all about enabling teams to deliver great software. In this demo-packed session, you will learn how to more effectively plan and track work by using the new Web-based project management tools; how to bridge the divide between development and operations by utilizing IntelliTrace in your production environments; and how to help keep team members on-task and 'in the zone', no matter how much there're randomized. See a tour of the new \"My Work\" and code review features. In addition to making your team more productive, we will show you how you can boost your overall code quality with new features such as code clone and an overhauled unit testing story in Visual Studio 11. With Team Foundation Server 11 you will see the full gamut of collaboration improvements, from the newly revamped Team Explorer, to the version control & build improvements. Want to work offline seamlessly? Wish merging happened less frequently & was simpler when it did? How about find work items faster? Join us to see all this and more."
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "Advanced SharePoint Web Part Development",
                                            SpeakerName = "Rob Windsor",
                                            Room = "Conv. Ctr. 6 (PubNub)",
                                            StartsAt = DateTime.Now,
                                            Track = "Open",
                                            Level = "Intermediate",
                                            Abstract = "Web Parts are the foundation of user interfaces in SharePoint. As a developer it's relatively easy (particularly with the Visual Web Part) to build something simple and get it deployed. But what do you do when you need to add editable properties or when you need to connect two Web Parts together? This fast-paced, demo-heavy session covers the more advanced aspects of building Web Parts for SharePoint 2010. We'll take a look at creating custom editor parts, building Visual Web Parts, constructing connected Web Parts, and how to render Web Parts asynchronously."
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "Finding your Customer DNA",
                                            SpeakerName = "Michael Neel",
                                            Room = "Conv. Ctr. 3 (CTS)  ",
                                            StartsAt = DateTime.Now,
                                            Track = "Database Platforms and Development",
                                            Level = "Intermediate",
                                            Abstract = "Want to make your Java code prettier? Guava is a Java library from Google full of elegant solutions to common problems. From a simple null check to a thread-safe self-populating cache, Guava has something for everyone. This easy-to-include, carefully-designed library is a veritable grab bag of handy Java tricks: sweet immutable collections; declarative, lazy-evaluating list processing; a better event handling pattern; and tigers! Take a quick tour of what Guava offers, with special focus on caching and list processing. Even if Guava is already in your project dependencies, you'll be surprised at all the juicy goodness inside."
                                        });
                group2.Sessions.Add(new Session
                                        {
                                            Title = "How to Ride the Service Bus with Azure",
                                            SpeakerName = "Andy Leonard",
                                            Room = "Conv. Ctr. 12 (Orasi)",
                                            StartsAt = DateTime.Now,
                                            Track = "Database Platforms and Development",
                                            Level = "Intermediate",
                                            Abstract = "Do you like a loosely coupled architecture? Are you considering a hybrid application between the cloud and on-premise solutions? Are you building mobile applications with notifications and events? The Azure Service Bus can make your life much easier!"
                                        });
                returnList.Add(group2);

                return returnList;
            }
        }
    }
}
