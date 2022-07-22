using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace KysectIntroTask
{
    [Serializable]
    public class ComplexTask : SimpleTask
    {
        private List<SimpleTask> _subtasks = new List<SimpleTask>();

        public DateTime? Deadline { get; }
        public IReadOnlyList<SimpleTask> Subtasks => _subtasks;

        public ComplexTask(string info, DateTime? deadline = null)
            : base(info)
        {
            Deadline = deadline;
        }
        public ComplexTask(string info, string deadline)
            : base(info)
        {
            Deadline = DateTime.Parse(deadline);
        }

        public void AddSubtask(string info)
        {
            _subtasks.Add(new SimpleTask(info));
        }
        public void RemoveSubtaskAt(int index)
        {
            _subtasks.RemoveAt(index);
        }
        public void RemoveSubtaskById(int id)
        {
            _subtasks.RemoveAll(subtask => subtask.Id == id);
        }
        public SimpleTask FindSubtaskById(int id)
        {
            return _subtasks.First(subtask => subtask.Id == id);
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder(" ");

            res.Append($"[{(Done ? 'x' : ' ')}] ");
            if (Deadline is not null)
                res.Append($"({Deadline}) ");
            res.Append($"{{{Id}}} {Info}");

            foreach (var subtask in _subtasks)
                res.Append($"\n{subtask.ToString()}");

            return res.ToString();
        }
    }
}