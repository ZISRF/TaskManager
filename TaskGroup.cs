using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace KysectIntroTask
{
    [Serializable]
    public class TaskGroup : Identifier
    {
        private List<ComplexTask> _tasks = new List<ComplexTask>();

        public string Name { get; }
        public IReadOnlyList<ComplexTask> Tasks => _tasks;

        public TaskGroup(string name)
        {
            Name = name;
        }

        public void AddTask(string info)
        {
            _tasks.Add(new ComplexTask(info));
        }
        public void AddTask(string info, string deadline)
        {
            _tasks.Add(new ComplexTask(info, deadline));
        }
        public void RemoveTaskAt(int index)
        {
            _tasks.RemoveAt(index);
        }
        public void RemoveTaskById(int id)
        {
            _tasks.RemoveAll(task => task.Id == id);
        }
        public ComplexTask FindTaskById(int id)
        {
            return _tasks.First(task => task.Id == id);
        }

        public List<ComplexTask> GetDoneTasks()
        {
            return _tasks.FindAll(task => task.Done == true);
        }
        public List<ComplexTask> GetUndoneTasks()
        {
            return _tasks.FindAll(task => task.Done == false);
        }
        public List<ComplexTask> GetTodayTasks()
        {
            return _tasks.FindAll(task => task.Deadline == DateTime.Now.Date);
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder($"Group {{{Id}}} {Name}: ");

            if (_tasks.Count == 0)
                res.Append("empty..");

            foreach (var task in _tasks)
                res.Append($"\n{task.ToString()}");

            return res.ToString();
        }
    }
}