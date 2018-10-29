using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.DomainService
{
    public interface IUserRepository
    {
        User ValidateUser(LoginInput input);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User UpdateUser(User user);
        User CreateNewUser(User user);
        void DeleteUser(User user);
    }
}