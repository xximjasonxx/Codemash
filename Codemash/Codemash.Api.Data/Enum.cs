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
        [EnumKey(".NET")] dotNET,
        [EnumKey("Design/UX")] DesignUX,
        Ruby,
        Testing,
        Agile,
        Mobile,
        JavaScript,
        Scala,
        Business,
        Craftsmanship,
        Python,
        [EnumKey("Game Development")] GameDevelopment,
        [EnumKey("Software Process")] SoftwareProcess,
        iOS,
        Wordpress,
        Other,
        [EnumKey("Mac/iPhone")] MaciPhone,
        Java,
        Web,
        Cloud,
        Hardware,
        Process,
        Security,
        [EnumKey("Windows 8")] Win8,
        Communication,
        Android,
        [EnumKey("Other Languages")] OtherLangs,
        PHP,
        [EnumKey("Continuous Deployment")] ContinuousDeployment,
        Clojure,
        [EnumKey("Public Speaking")] PublicSpeaking
    }

    public enum Room
    {
        Unknown,
        [EnumKey("Indigo Bay")] IndigoBay
    }

    public enum ChangeAction
    {
        Add,
        Modify,
        Delete
    }
}
