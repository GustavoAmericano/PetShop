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
            return _ctx.Users.FirstOrDefault(u => string.Equals(u.Username, input.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _ctx.Users;
        }

        public User GetUserById(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(UserInput userInput)
        {
            return null;
        }

        public User CreateNewUser(UserInput userInput)
        {
            throw new NotImplementedException();
        }


        public void DeleteUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}