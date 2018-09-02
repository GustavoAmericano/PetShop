using System;
using System.Linq;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop.OwnerUI
{
    public class OwnerUI
    {
        private readonly IOwnerService _ownerService;
        private readonly IPetService _petService;

        public OwnerUI(IOwnerService ownerService, IPetService petService)
        {
            _ownerService = ownerService;
            _petService = petService;
        }

        public void ShowMenu()
        {
            bool isActive = true;
            while (isActive)
            {
                Console.Clear();
                Console.WriteLine("\u001b[32m1.\u001b[0m Show all owners\r\n" +
                                  "\u001b[32m2.\u001b[0m Search owners\r\n" +
                                  "\u001b[32m3.\u001b[0m Create owner\r\n" +
                                  "\r\n\r\nType \u001b[32mindex\u001b[0m of the action" +
                                  "\r\nType \u001b[31mback\u001b[0m to return to previous menu.\r\n");
                Console.Write("Input: ");
                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "back":
                        return;
                    case "1":
                        LoadAllOwners();
                        break;
                    case "2":
                        SearchOwners();
                        break;
                    case "3":
                        CreateOwner();
                        break;
                }
            }
        }

        private void CreateOwner()
        {
            Console.Write("Enter first name: ");
            var fName = Console.ReadLine();
            Console.Write("Enter last name(s): ");
            var lName = Console.ReadLine();
            Console.Write("Enter address: ");
            var address = Console.ReadLine();
            Console.Write("Enter email: ");
            var email = Console.ReadLine();
            Console.Write("Enter phonenumber: ");
            var pNumber= Console.ReadLine();

            _ownerService.CreateOwner(new Owner
            {
                FirstName = fName,
                LastName = lName,
                Address = address,
                Email = email,
                PhoneNumber = pNumber
            });
        }

        private void SearchOwners()
        {
            throw new NotImplementedException();
        }

        private void LoadAllOwners()
        {
            _ownerService.GetAllOwners().ToList().ForEach(PrintOwner);
            Console.ReadLine();
        }

        private void PrintOwner(Owner o)
        {
            Console.WriteLine(
                "Name: " + o.FirstName + " " + o.LastName +
                "Address: " + o.Address +
                "Email: " + o.Email + 
                "Phone: " + o.PhoneNumber + 
                "Pets: " 
                );
            _petService.GetPetsByOwnerId(o.Id).ToList().ForEach(x => Console.WriteLine(x.Name + " (" + x.Id + ")"));
        }
    }
}