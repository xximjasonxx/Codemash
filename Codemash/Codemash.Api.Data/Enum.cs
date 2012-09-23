using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data
{
    public enum Level
    {
        Unknown,
        General,
        Intermediate,
        Advanced,
        Beginner,
    }

    public enum Track
    {
        Unknown,
        Keynote,
        [EnumKey("Platforms and Tools")] PlatformsTools,
        [EnumKey("Developer Tools, Languages, and Frameworks")] DeveloperToolsLanguagesFrameworks,
        Open,
        Web,
        [EnumKey("Database Platforms and Development")] DatabasePlatformsDevelopment,
        [EnumKey("Mobile Development")] MobileDevelopment,
        Cloud,
        Sharepoint,
        Architecture,
        [EnumKey("Development Practices")] DevelopmentPractices,
        [EnumKey("XNA and Kinect")] XNAKinect,
        [EnumKey("Test Frameworks and Methodology")] TestFrameworks,
        [EnumKey("Professional Development")] ProfessionalDevelopment,
        [EnumKey("Project Management and Methodology")] ProjectManagement
    }

    public enum Room
    {
        Unknown,
        [EnumKey("Conv. Ctr. E, F, G")] CtrEFG,
        [EnumKey("Conv. Ctr. 4 (ComponentOne)")] Ctr4,
        [EnumKey("Conv. Ctr. 13 (GitHub)")] Ctr13,
        [EnumKey("Conv. Ctr. 6 (PubNub)")] Ctr6,
        [EnumKey("Conv. Ctr. F (Microsoft)")] CtrF,
        [EnumKey("Conv. Ctr. E (Telerik)")] CtrE,
        [EnumKey("Conv. Ctr. G (Pearson)")] CtrG,
        [EnumKey("Conv. Ctr. 3 (CTS)")] Ctr3,
        [EnumKey("Conv. Ctr. 12 (Orasi)")] Ctr12,
        [EnumKey("Conv. Ctr. 5 (Wintellect)")] Ctr5,
        [EnumKey("Conv. Ctr. 7, 8 (Twilio)")] Ctr78,
        [EnumKey("Conv. Ctr. 14 (Infragistics)")] Ctr14,
        [EnumKey("Conv. Ctr. 9, 10 (TEKsystems)")] Ctr910
    }

    public enum ChangeAction
    {
        Add,
        Modify,
        Delete
    }
}
