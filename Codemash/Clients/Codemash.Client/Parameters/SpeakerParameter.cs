using Codemash.Client.Data.Entities;

namespace Codemash.Client.Parameters
{
    public class SpeakerParameter
    {
        public Speaker Value { get; private set; }

        public SpeakerParameter(Speaker speaker)
        {
            Value = speaker;
        }
    }
}
