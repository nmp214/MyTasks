using System.Threading.Tasks;
using MyTasks.Models;
using MyTasks.Controllers;

namespace MyTasks.Interfaces
{
    public interface ITask
    {
        public List<MyTask> GetAll();

        public MyTask Get(int id);

        public void Add(MyTask task);

        public bool Update(int id, MyTask newTask);

        public bool Delete(int id);
    }
}