using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService
{
    public interface IUserService
    {
        User ValidateUser(LoginInput input);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User UpdateUser(UserInput userInput);
        User CreateNewUser(UserInput userInput);
        void DeleteUser(User user);
    }
}