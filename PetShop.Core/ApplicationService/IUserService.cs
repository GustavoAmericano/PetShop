using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService
{
    public interface IUserService
    {
        User ValidateUser(LoginInput input);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User UpdateUser(UpdateUserInput userInput);
        User CreateNewUser(CreateUserInput userInput);
        void DeleteUser(User user);
    }
}