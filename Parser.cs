using System;

namespace KysectIntroTask
{
    internal static class Parser
    {
        public const string FileName = "TaskManager.json";

        public static void Main(string[] args)
        {
            var taskManager = TaskManager.ReadFromFile(FileName);

            try
            {
                if (args.Length == 0)
                    throw new Exception("No arguments, try \"help\" argument");

                switch (args[0])
                {
                    case "help" when args.Length == 1:
                        Console.WriteLine("Commands:");
                        Console.WriteLine(" show");
                        Console.WriteLine(" clear");
                        Console.WriteLine(" get-done-tasks");
                        Console.WriteLine(" get-undone-tasks");
                        Console.WriteLine(" get-today-tasks");

                        Console.WriteLine(" add-task *info*");
                        Console.WriteLine(" add-task *info* *deadline*");
                        Console.WriteLine(" add-task *info* to-group *group-id*");
                        Console.WriteLine(" add-task *info* *deadline* to-group *group-id*");
                        Console.WriteLine(" remove-task *task-id*");
                        Console.WriteLine(" remove-task *task-id* from-group *group-id*");
                        Console.WriteLine(" do-task *task-id*");
                        Console.WriteLine(" do-task *task-id* from-group *group-id*");

                        Console.WriteLine(" add-subtask *info* to-task *task-id*");
                        Console.WriteLine(" add-subtask *info* to-task *task-id* from-group *group-id*");
                        Console.WriteLine(" remove-subtask *subtask-id* from-task *task-id*");
                        Console.WriteLine(" remove-subtask *subtask-id* from-task *task-id* from-group *group-id*");
                        Console.WriteLine(" do-subtask *subtask-id* from-task *task-id*");
                        Console.WriteLine(" do-subtask *subtask-id* from-task *task-id* from-group *group-id*");

                        Console.WriteLine(" add-group *name*");
                        Console.WriteLine(" remove-group *group-id*");
                        break;

                    case "show" when args.Length == 1:
                        Console.WriteLine(taskManager);
                        break;

                    case "clear" when args.Length == 1:
                        taskManager.Clear();
                        Console.WriteLine("All tasks and groups removed");
                        break;

                    case "get-done-tasks" when args.Length == 1:
                        foreach (var task in taskManager.GetDoneTasks())
                            Console.WriteLine(task);
                        break;

                    case "get-undone-tasks" when args.Length == 1:
                        foreach (var task in taskManager.GetUndoneTasks())
                            Console.WriteLine(task);
                        break;

                    case "get-today-tasks" when args.Length == 1:
                        foreach (var task in taskManager.GetTodayTasks())
                            Console.WriteLine(task);
                        break;

                    case "add-task" when args.Length == 2:
                        taskManager.AddTask(args[1]);
                        Console.WriteLine($"Task created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "add-task" when args.Length == 3:
                        taskManager.AddTask(args[1], args[2]);
                        Console.WriteLine($"Task created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "add-task" when args.Length == 4 && args[2] == "to-group":
                        taskManager.FindGroupById(int.Parse(args[3])).AddTask(args[1]);
                        Console.WriteLine($"Task created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "add-task" when args.Length == 5 && args[3] == "to-group":
                        taskManager.FindGroupById(int.Parse(args[4])).AddTask(args[1], args[2]);
                        Console.WriteLine($"Task created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "remove-task" when args.Length == 2:
                        taskManager.RemoveTaskById(int.Parse(args[1]));
                        break;

                    case "remove-task" when args.Length == 4 && args[2] == "from-group":
                        taskManager.FindGroupById(int.Parse(args[3]))
                        .RemoveTaskById(int.Parse(args[1]));
                        break;

                    case "do-task" when args.Length == 2:
                        taskManager.FindTaskById(int.Parse(args[1])).Done = true;
                        break;

                    case "do-task" when args.Length == 4 && args[2] == "from-group":
                        taskManager.FindGroupById(int.Parse(args[3]))
                        .FindTaskById(int.Parse(args[1])).Done = true;
                        break;

                    case "add-subtask" when args.Length == 4 && args[2] == "to-task":
                        taskManager.FindTaskById(int.Parse(args[3])).AddSubtask(args[1]);
                        Console.WriteLine($"Subtask created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "add-subtask" when args.Length == 6 && args[2] == "to-task" && args[4] == "from-group":
                        taskManager.FindGroupById(int.Parse(args[5]))
                        .FindTaskById(int.Parse(args[3])).AddSubtask(args[1]);
                        Console.WriteLine($"Subtask created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "remove-subtask" when args.Length == 4 && args[2] == "from-task":
                        taskManager.FindTaskById(int.Parse(args[3])).AddSubtask(args[1]);
                        break;

                    case "remove-subtask" when args.Length == 6 && args[2] == "from-task" && args[4] == "from-group":
                        taskManager.FindGroupById(int.Parse(args[5]))
                        .FindTaskById(int.Parse(args[3])).AddSubtask(args[1]);
                        break;

                    case "do-subtask" when args.Length == 4 && args[2] == "from-task":
                        taskManager.FindTaskById(int.Parse(args[3]))
                        .FindSubtaskById(int.Parse(args[1])).Done = true;
                        break;

                    case "do-subtask" when args.Length == 6 && args[2] == "from-task" && args[4] == "from-group":
                        taskManager.FindGroupById(int.Parse(args[5]))
                        .FindTaskById(int.Parse(args[3])).FindSubtaskById(int.Parse(args[1])).Done = true;
                        break;

                    case "add-group" when args.Length == 2:
                        taskManager.AddGroup(args[1]);
                        Console.WriteLine($"Group created! Id: {Identifier.MaxUsedId}");
                        break;

                    case "remove-group" when args.Length == 2:
                        taskManager.RemoveGroupById(int.Parse(args[1]));
                        break;

                    default:
                        throw new Exception("Invalid arguments, try \"help\" argument");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                taskManager.SaveToFile(FileName);
            }
        }
    }
}