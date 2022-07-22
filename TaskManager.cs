using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace KysectIntroTask
{
    [Serializable]
    public class TaskManager
    {
        private List<ComplexTask> _tasks = new List<ComplexTask>();
        private List<TaskGroup> _groups = new List<TaskGroup>();

        public IReadOnlyList<ComplexTask> Tasks => _tasks;
        public IReadOnlyList<TaskGroup> Groups => _groups;

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

        public void AddGroup(string name)
        {
            _groups.Add(new TaskGroup(name));
        }
        public void RemoveGroupAt(int index)
        {
            _groups.RemoveAt(index);
        }
        public void RemoveGroupById(int id)
        {
            _groups.RemoveAll(group => group.Id == id);
        }
        public TaskGroup FindGroupById(int id)
        {
            return _groups.First(group => group.Id == id);
        }

        public void SaveToFile(string path)
        {
            var formatter = new DataContractJsonSerializer(typeof(TaskManager));
            using (var file = File.Open(path, FileMode.Create))
            {
                formatter.WriteObject(file, this);
            }
        }
        public static TaskManager ReadFromFile(string path)
        {
            try
            {
                var formatter = new DataContractJsonSerializer(typeof(TaskManager));
                using (var file = File.Open(path, FileMode.OpenOrCreate))
                {
                    var res = formatter.ReadObject(file) as TaskManager;
                    return res ?? new TaskManager();
                }
            }
            catch (SerializationException)
            {
                return new TaskManager();
            }
        }
        public void Clear()
        {
            _tasks.Clear();
            _groups.Clear();
        }

        public List<ComplexTask> GetDoneTasks()
        {
            var res = _tasks.FindAll(task => task.Done == true);
            foreach (var group in _groups)
                res.AddRange(group.GetDoneTasks());
            return res;
        }
        public List<ComplexTask> GetUndoneTasks()
        {
            var res = _tasks.FindAll(task => task.Done == false);
            foreach (var group in _groups)
                res.AddRange(group.GetUndoneTasks());
            return res;
        }
        public List<ComplexTask> GetTodayTasks()
        {
            var res = _tasks.FindAll(task => task.Deadline == DateTime.Now.Date);
            foreach (var group in _groups)
                res.AddRange(group.GetTodayTasks());
            return res;
        }

        public override string ToString()
        {
            if (_tasks.Count == 0 && _groups.Count == 0)
                return "No tasks..";

            StringBuilder res = new StringBuilder();

            if (_tasks.Count != 0)
                res.Append("Main tasks:");

            foreach (var task in _tasks)
                res.Append($"\n{task.ToString()}");

            foreach (var group in _groups)
                res.Append($"\n{group.ToString()}");

            return res.ToString();
        }
    }
}