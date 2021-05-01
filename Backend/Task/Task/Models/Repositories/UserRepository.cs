using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        TaskContext context;
        public UserRepository(TaskContext _context)
        {
            context = _context;
        }
        public void Add(User NewUser)
        {
            context.Users.Add(NewUser);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            User UserInDb =context.Users.FirstOrDefault(u => u.UserID == id);
            if(UserInDb != null)
            {
                context.Users.Remove(UserInDb);
                context.SaveChanges();
            }
            
        }

        public User Find(int id)
        {
            User UserInDb = context.Users.FirstOrDefault(u => u.UserID == id);
            return UserInDb;
        }

        public IList<User> Get()
        {
            IList<User> UserList = context.Users.ToList();
            return UserList;
        }

        public User Login(User user)
        {
            User userInDb = context.Users.FirstOrDefault(u => u.Email == user.Email||u.Password==user.Password);
            return userInDb;
        }

        public IList<User> Search(string term)
        {
            IList<User> ResultSearch = context.Users
                .Where
                (
                    u => u.Name.Contains(term) ||
                         u.Email.Contains(term) ||
                         u.PhoneNumber.Contains(term)
                ).ToList();
            return ResultSearch;
        }

        public void Update(int id, User NewUser)
        {
            User UserInDb = context.Users.FirstOrDefault(u => u.UserID == id);
            if (UserInDb != null)
            {
                UserInDb.Name = NewUser.Name;
                UserInDb.Email = NewUser.Email;
                UserInDb.PhoneNumber = NewUser.PhoneNumber;
                UserInDb.Password = NewUser.Password;
                context.SaveChanges();
            }

        }
    }
}
