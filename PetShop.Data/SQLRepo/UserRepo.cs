using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Data.SQLRepo
{
    public class UserRepo : IUserRepository
    {
        private readonly PetShopContext _ctx;

        public UserRepo(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public User ValidateUser(LoginInput input)
        {
            return _ctx.Users
                .FirstOrDefault(u => string.Equals(u.Username, input.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _ctx.Users.ToList().Select(x =>
            {
                x.PasswordHash = null;
                x.PasswordSalt = null;
                return x;
            });

        }

        public User GetUserById(int id)
        {
            User user = _ctx.Users.FirstOrDefault(u => u.Id == id);
            _ctx.Entry(user).State = EntityState.Detached;
            return user;
        }

        public User UpdateUser(User user)
        {
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.SaveChanges();
            return user;
        }

        public User CreateNewUser(User user)
        {
            _ctx.Entry(user).State = EntityState.Added;
            _ctx.SaveChanges();
            return user;
        }


        public void DeleteUser(User user)
        {
            _ctx.Entry(user).State = EntityState.Deleted;
            _ctx.SaveChanges();
        }
    }
}