using MyTasks.Models;
using MyTasks.Interfaces;

namespace MyTasks.Services
{
    public class TaskService : ITask
    {
        private List<MyTask> tasks = new List<MyTask>
        {
            new MyTask (1, "make a list", true),
            new MyTask (2, "go to the bank", false),
            new MyTask (3, "wash dishes", true),
            new MyTask (4, "buy vegtables", false)
        };

        public List<MyTask> GetAll() => tasks;

        public void Add(MyTask task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
        }

        public MyTask Get(int id) => tasks.FirstOrDefault(t => t.Id == id);


        public bool Update(int id, MyTask newTask)
        {
            if (newTask.Id != id)
                return false;

            var task = tasks.FirstOrDefault(t => t.Id == id);
            task.Name = newTask.Name;
            task.IsDone = newTask.IsDone;
            return true;
        }
        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            return true;
        }
    }
}