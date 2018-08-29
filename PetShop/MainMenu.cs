using System;
using System.Linq;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop
{
    public class MainMenu
    {
        private readonly IPetService _petService;

        public MainMenu(IPetService petService)
        {
            _petService = petService;
            Console.Write("Input: ");
            var input = Console.ReadLine();

            if (input == "1")
            {
                _petService.GetAllPets().ToList().ForEach(Console.WriteLine);
            }
            else if(input == "2")
            {
                Console.Clear();
                Console.Write("\r\nEnter name of pet: ");
                var name = Console.ReadLine();
                Console.Write("\r\nEnter type of pet: ");
                var type = Console.ReadLine();
                Console.Write("\r\nEnter color of pet: ");
                var color = Console.ReadLine();
                Console.Write("\r\nEnter price of pet: ");
                var priceAsString = Console.ReadLine();
                Double price;
                while (!Double.TryParse(priceAsString, out price))
                {
                    Console.Write("\r\nInvalid input. Numbers only: ");
                    priceAsString = Console.ReadLine();
                }

                Console.WriteLine(_petService.CreatePet(new Pet()
                {
                    Name = name,
                    PetType = type,
                    Color = color,
                    Price = price
                }));
                Console.ReadLine();
            }
        }
    }
}