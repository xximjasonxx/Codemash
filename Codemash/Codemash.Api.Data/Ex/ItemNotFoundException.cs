using System;

namespace Codemash.Api.Data.Ex
{
    public class ItemNotFoundException : Exception
    {
        public string ItemType { get; private set; }
        public object Identifier { get; private set; }

        public ItemNotFoundException(string itemType, object identifer)
            : base(string.Format("Item of Type {0} identified with {1} not found in repository", itemType, identifer))
        {
            ItemType = itemType;
            Identifier = identifer;
        }
    }
}
