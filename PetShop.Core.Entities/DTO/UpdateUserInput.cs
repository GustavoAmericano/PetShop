namespace PetShop.Core.Entities
{
    public class UpdateUserInput
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public bool isAdmin { get; set; }
    }
}