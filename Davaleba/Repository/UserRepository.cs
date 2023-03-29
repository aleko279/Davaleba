using Davaleba.Helpers;
using Davaleba.Interface;
using Davaleba.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Davaleba.Repository
{
    public class UserRepository : IUsers
    {
        private DavalebaContext _davalebaContext;
        private readonly IMapper _mapper;
        public UserRepository(DavalebaContext davalebaContext, IMapper mapper)
        {
            _davalebaContext = davalebaContext;
            _mapper = mapper;
        }
        //public List<User> GetUsers()
        //{
        //    return _davalebaContext.Users.ToList();
        //}
        public List<User> GetUsers()
        {
            return _davalebaContext.Users.ToList(); ;
        }
        public User GetUser(int id)
        {
            var user = _davalebaContext.Users.Find(id);
            if (user == null)
            {
                throw new AppException("User Not Found");
            }
            return user;
        }
        public void AddUser(UserCustomClass userCustomClass)
        {
            var userExist = _davalebaContext.Users.Any(m => m.UserName == userCustomClass.UserName);
            if (userExist == true)
            {
                throw new AppException("User UserName: " + userCustomClass.UserName + " Already Exist");
            }
            var user = _mapper.Map<User>(userCustomClass);
            _davalebaContext.Add(user);
            _davalebaContext.SaveChanges();
        }
        public void UpdateUser(UserCustomClass user)
        {

            //_davalebaContext.Users.Update(user);
            _davalebaContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void DeleteUser(int id)
        {
            var user = _davalebaContext.Users.Find(id);
            if (user == null)
            {
                throw new AppException("User Not Found");
            }
            _davalebaContext.Users.Remove(user);
            _davalebaContext.SaveChanges();
        }
    }
}

