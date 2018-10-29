namespace PetShop.Core.Entities
{
    public class UserInput
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}