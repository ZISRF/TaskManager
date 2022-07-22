using System;
using System.Runtime.Serialization;

namespace KysectIntroTask
{
    [Serializable]
    public abstract class Identifier
    {
        public int Id { get; }
        public static int MaxUsedId { get; private set; } = 0;

        public Identifier()
        {
            Id = ++MaxUsedId;
        }

        [OnDeserialized]
        private void fixMaxUsedId(StreamingContext context)
        {
            if (MaxUsedId < Id)
            {
                MaxUsedId = Id;
            }
        }
    }
}