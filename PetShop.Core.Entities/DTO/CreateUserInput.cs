namespace PetShop.Core.Entities
{
    public class CreateUserInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}