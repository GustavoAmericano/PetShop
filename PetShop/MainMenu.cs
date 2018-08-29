using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop
{
    public class MainMenu
    {
        private readonly IPetService _petService;
        private bool _isRunning = true;
        public MainMenu(IPetService petService)
        {
            _petService = petService;
            ShowMenu();

        }

        private void ShowMenu()
        {
            while (_isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Show all pets\r\n" + 
                                  "2. Show pets sorted by price\r\n" + 
                                  "3. Create new pet.\r\n" +
                                  "4. Show 5 cheapest pets" +
                                  "\r\n\r\n" +
                                  "Type \"exit\" to exit the program.\r\n ");
                Console.Write("Input: ");
                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "1":
                        LoadAllPets();
                        break;

                    case "2":
                        ShowPetsPriceSort();
                        break;
                    case "3":
                        GetFiveCheapest();
                        break;

                    case "4":
                        CreatePet();
                        break;

                    case "exit":
                        _isRunning = false;
                        break;

                    default:
                        Console.Write("Unrecognized input. Press any key and try again.");
                        Console.ReadKey();
                        break;
                }
            }
    }

        private void ShowPetsPriceSort()
        {
            IEnumerable<Pet> temp = new List<Pet>();
            Console.Clear();
            Console.Write("1. Cheapest -> Highest\r\n2. Highest -> Cheapest\r\nInput:");
            string input = Console.ReadLine();

            while ((!input.Equals("1") && !input.Equals("2")))
            {
                Console.Write("Invalid input.\r\nType 1 for ascending order, or 2 for descending order. \r\nInput:");
                input = Console.ReadLine();
            }

            Console.Clear();

            switch (input)
            {
                case "1":
                    temp = _petService.GetPetsPriceSort(true);
                    break;
                case "2":
                   temp = _petService.GetPetsPriceSort(false);
                    break;
            }

            temp.ToList().ForEach(x =>
            {
                PrintPet(x);
                Console.WriteLine();
            });
            Console.ReadLine();
        }

        private void GetFiveCheapest()
        {
            Console.Clear();
            _petService.GetFiveCheapest().ToList().ForEach(x =>
            {
                PrintPet(x);
                Console.WriteLine();
            });
            Console.ReadLine();
        }

        private void LoadAllPets()
        {
            Console.Clear();
            List<Pet> pets = _petService.GetAllPets().ToList();
            pets.ForEach(x =>
            {
                PrintPet(x);
                Console.WriteLine();
            });
            Pet pet = SelectPet(pets);
            if (pet != null)
            {
                InteractWithPet(pet);
            }            
        }

        private void InteractWithPet(Pet pet)
        {
            Console.Clear();
            PrintPet(pet);
            Console.Write("\r\n1. Edit pet" + 
                          "\r\n2. Delete pet" + 
                          "\r\n\r\nType back to return to main menu." +
                          "\r\n\r\nInput: "
                          );
            string input = Console.ReadLine();

            switch (input)
            {
                case "back":
                    return;
                case "1":
                    EditPet(pet);
                    break;
                case "2":
                    _petService.DeletePet(pet);
                    break;
                default:
                    break;
            }

        }

        private void EditPet(Pet pet)
        {
            Pet localPet = pet;
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.Write(
                    "\r\n1. Name: " +  localPet.Name +
                    "\r\n2. Type: " + localPet.PetType +
                    "\r\n3. Color: " + localPet.Color +
                    "\r\n4. Date of Birth: " + localPet.BirthDate +
                    "\r\n5. Sold : " + localPet.SoldDate +
                    "\r\n6. Price: " + localPet.Price +
                    "\r\n\r\nType index of information you want to change" +
                    "\r\nType  end to stop editing" +
                    "\r\nInput: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "end":
                        active = false;
                        break;
                    case "1":

                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    case "6":

                        break;

                }

            }
        }

        private void CreatePet()
        {
            Console.Clear();
            Console.Write("\r\nEnter name of pet: ");
            var name = Console.ReadLine();
            Console.Write("\r\nEnter type of pet: ");
            var type = Console.ReadLine();
            Console.Write("\r\nEnter color of pet: ");
            var color = Console.ReadLine();

            Console.Write("\r\nEnter DOB of pet: ");
            var dobString = Console.ReadLine();
            DateTime dob;
            while (!DateTime.TryParse(dobString, out dob))
            {
                Console.Write("Invalid input. Accepted format is e.g: 10-10-2010.\r\nInput:");
                dobString = Console.ReadLine();
            }

            Console.Write("\r\nEnter price of pet: ");
            var priceAsString = Console.ReadLine();
            Double price;
            while (!Double.TryParse(priceAsString, out price))
            {
                Console.Write("\r\nInvalid input. Numbers only: ");
                priceAsString = Console.ReadLine();
            }

            PrintPet(_petService.CreatePet(new Pet()
            {
                Name = name,
                PetType = type,
                Color = color,
                Price = price,
                BirthDate = dob,
                
            }));
            Console.ReadLine();
        }

        private void PrintPet(Pet pet)
        {
            String sold;
            if (pet.SoldDate.Year == 0001)
            {
                sold = "No";
            }
            else
            {
                sold = pet.SoldDate.ToString();
            }
            Console.WriteLine(
                "Name: " + pet.Name + "  (" + pet.Id + ")" +
                "\r\nType: " + pet.PetType +
                "\r\nColor: " + pet.Color +
                "\r\nDOB: " + pet.BirthDate +
                "\r\nSold: " + sold +
                "\r\nPrice: " + pet.Price
            );
        }

        private Pet SelectPet(IEnumerable<Pet> pets)
        {
            Pet pet = null;
            Console.Write("Type index of pet you wish to interact with.\r\n" +
                              "Type back to go back to main menu. \r\n\r\n" +
                              "Input: ");
            bool active = true;
            while (active)
            {
                string input = Console.ReadLine();

                int Id;
                if (input.ToLower().Equals("back"))
                {
                    return null;
                }
                else if (int.TryParse(input, out Id))
                {
                    if (pets.Any(x => x.Id == Id))
                    {
                        pet = pets.First(x => x.Id == Id);
                    }
                    if (pet != null)
                    {
                        active = false;
                    }
                    else
                    {
                        Console.WriteLine("Pet with ID " + Id + " does not exist in current context.");
                        Console.Write("Type index of pet you wish to interact with.\r\n" +
                                      "Type back to go back to main menu. \r\n\r\n" +
                                      "Input: ");
                    }
                }
                else
                {
                    Console.Write("Invalid Input. Please retry. \r\nInput: ");
                    Console.Write("Type index of pet you wish to interact with.\r\n" +
                                  "Type back to go back to main menu. \r\n\r\n" +
                                  "Input: ");
                }
            }

            return pet;
        }
    }
}