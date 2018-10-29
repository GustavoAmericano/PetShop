using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            if(!user.PasswordHash
                .Equals(GetHashValue(input.Password + user.PasswordSalt)))
                    throw new UnauthorizedAccessException();
            user.LastLogin = DateTime.Now;
            _userRepo.UpdateUser(user);
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

        public User UpdateUser(UpdateUserInput userInput)
        {
            // Fetch existing version of user from DB
            User user = _userRepo.GetUserById(userInput.Id);
            // Check if user was actually found, and that their ID equals the inputs one.
            //If not, throw exception
            if(user == null || user.Id != userInput.Id)
                throw new ArgumentException();
            // Ensure that inputs 'oldPassword' is actually the same as the stored one.
            // If not, throw exception.
            if(user.PasswordHash != GetHashValue(userInput.oldPassword + user.PasswordSalt))
                throw new ArgumentException();

            var salt = user.PasswordSalt; // Set to currently stored salt
            var pass = user.PasswordHash; // Set to currently stored hash
            //If a new password is in the input:
            //1. Generate a new salt, so we don't use the same.
            //2. Generate new hash with 'newPassword' and the newly generated salt.
            if (userInput.newPassword != null || userInput.newPassword != "")
            {
                salt = GenerateSalt();
                pass = GetHashValue(userInput.newPassword + salt);
            }
            
            //Finally, create the updated user object
            user = new User()
            {
                Id = userInput.Id,
                Username = userInput.Username,
                PasswordHash = pass,
                PasswordSalt = salt,
                IsAdmin = userInput.isAdmin
            };
            //Update user in DB and return the new object.
            return _userRepo.UpdateUser(user);
        }

        public User CreateNewUser(CreateUserInput userInput)
        {
            var salt = GenerateSalt();

            User user = new User
            {
                Username = userInput.Username,
                IsAdmin = userInput.isAdmin,
                PasswordSalt = salt,
                CreationDate = DateTime.Now,
                LastLogin = DateTime.MinValue,
                PasswordHash = GetHashValue(userInput.Password + salt)
            };

            return (_userRepo.CreateNewUser(user));
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
    }
}