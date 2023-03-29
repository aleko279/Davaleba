using Davaleba.Models;
namespace Davaleba.Interface
{
    public interface IUsers
    {
        //public List<User> GetUsers();
        List<User> GetUsers();
        public User GetUser(int id);
        public void AddUser(UserCustomClass user);
        public void UpdateUser(UserCustomClass user);
        public void DeleteUser(int id);

    }
}
