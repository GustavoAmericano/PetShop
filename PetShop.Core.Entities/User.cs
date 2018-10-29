using System;
using System.ComponentModel;

namespace PetShop.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsAdmin { get; set; }
    }
}