using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User ValidateUser(LoginInput input)
        {
            User user = _userRepo.ValidateUser(input);
            if(user == null) throw new UnauthorizedAccessException();
            if(user.PasswordHash != GetHashValue(input.Password + user.PasswordSalt)) throw new UnauthorizedAccessException();
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);
        }

        public User UpdateUser(UserInput userInput)
        {
            return _userRepo.UpdateUser(userInput);
        }

        public User CreateNewUser(UserInput userInput)
        {
            return _userRepo.CreateNewUser(userInput);
        }


        public void DeleteUser(User user)
        {
            _userRepo.DeleteUser(user);
        }

        private string GenerateSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private string GetHashValue(string input)
        {
            using (var sha = SHA256Managed.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(bytes);
            }
        }

        private bool ValidateUserData(User user)
        {
            if (user.Username == null) return false;

            return true;
        }
    }
}