using System.Threading.Tasks;
using MyTasks.Models;
using MyTasks.Controllers;

namespace MyTasks.Interfaces
{
    public interface IUser
    {
        public User Login(User user);
        public List<User> GetAll();

        public User Get(int id);

        public void Add(User user);

        public bool Update(int id, User user);

        public bool Delete(int id);
    }
}