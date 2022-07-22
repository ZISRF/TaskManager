using System;

namespace KysectIntroTask
{
    [Serializable]
    public class SimpleTask : Identifier
    {
        public string Info { get; }
        public bool Done { get; set; } = false;

        public SimpleTask(string info)
        {
            Info = info;
        }

        public override string ToString() => this switch
        {
            { Done: true } => $"   - [x] {{{Id}}} {Info}",
            { Done: false } => $"   - [ ] {{{Id}}} {Info}"
        };
    }
}