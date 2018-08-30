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
                                  "2. Search pets by type\r\n" +
                                  "3. Show pets sorted by price\r\n" +
                                  "4. Show 5 cheapest pets\r\n" +
                                  "5. Create new pet" +
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
                        SearchPetByType();
                        break;
                    case "3":
                        ShowPetsPriceSort();
                        break;
                    case "4":
                        GetFiveCheapest();
                        break;
                    case "5":
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
            Console.Write("1. Cheapest -> Highest\r\n2. Highest -> Cheapest" +
                          "\r\nType back to return to previous menu\r\nInput:");
            string input = Console.ReadLine();

            while ((!input.Equals("1") && !input.Equals("2")))
            {
                Console.Write("Invalid input.\r\nType 1 for ascending order, or 2 for descending order." +
                              " \r\nType back to return to previous menu\r\nInput:");
                input = Console.ReadLine();
            }
            Console.Clear();
            switch (input)
            {
                case "back":
                    return;
                case "1":
                    while (InteractWithPet(_petService.GetPetsPriceSort(true).ToList()));
                    break;
                case "2":
                    while (InteractWithPet(_petService.GetPetsPriceSort(false).ToList()));
                    break;
            }
        }

        private void GetFiveCheapest()
        {
            while (InteractWithPet(_petService.GetFiveCheapest().ToList()));
        }

        private void LoadAllPets()
        {
            while (InteractWithPet(_petService.GetAllPets().ToList()));
        }

        private void SearchPetByType()
        {
            
            Console.Clear();
            Console.Write("Enter type or part of type. \r\nInput:");
            string input = Console.ReadLine();
            while (InteractWithPet(_petService.SearchPetsByType(input).ToList()));
           
        }

        private bool InteractWithPet(List<Pet> pets)
        {
            Console.Clear();
            pets.ForEach(x =>
            {
                PrintPet(x);
                Console.WriteLine();
            });
            Pet pet = SelectPet(pets);
            if(pet == null) return false;
            Console.Clear();
            PrintPet(pet);
            Console.Write("\n1. Edit pet" + 
                          "\n2. Delete pet" + 
                          "\n\nType back to return to main menu." +
                          "\n\nInput: "
                          );
            string input = Console.ReadLine();

            switch (input)
            {
                case "back":
                    return true;
                case "1":
                    EditPet(pet);
                    break;
                case "2":
                    _petService.DeletePet(pet);
                    break;
                default:
                    return true;
            }

            return true;
        }

        private void EditPet(Pet pet)
        {
            Pet localPet = new Pet(pet);
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.Write(
                    "\r\n1. Name: " + localPet.Name +
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
                        Console.Write("Enter new name of pet: ");
                        localPet.Name = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Enter new type of pet: ");
                        localPet.PetType = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Enter new color of pet: ");
                        localPet.Color = Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Enter new DOB of pet: ");
                        localPet.BirthDate = GetValidDate();
                        break;
                    case "5":
                        Console.Write("Enter new sold date of pet: ");
                        localPet.SoldDate = GetValidDate();
                        break;
                    case "6":
                        Console.Write("\r\nEnter price of pet: ");
                    localPet.Price = GetValidPrice();
                        break;
                }
            }

            if (pet.Equals(localPet))
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.Write("Changes were made to the pet." +
                              "\r\nDo you wish to save the changes?(Y/N)" +
                              "\r\nInput:");
                string input = Console.ReadLine();
                if (input.ToLower().Equals("y") || input.ToLower().Equals("n"))
                {
                    if (input.ToLower().Equals("y"))
                        _petService.SavePet(localPet);
                    else return;
                }
            }
        }

        private double GetValidPrice()
        {
            var priceAsString = Console.ReadLine();
            Double price;
            while (!Double.TryParse(priceAsString, out price))
            {
                Console.Write("\r\nInvalid input. Numbers only: ");
                priceAsString = Console.ReadLine();
            }
            return price;
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
            var dob = GetValidDate();
            Console.Write("\r\nEnter price of pet: ");
            Double price = GetValidPrice();
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

        private DateTime GetValidDate()
        {
            string dateString = Console.ReadLine();
            DateTime date;
            while (!DateTime.TryParse(dateString, out date))
            {
                Console.Write("Invalid input. Accepted format is e.g: 10-10-2010.\r\nInput:");
                dateString = Console.ReadLine();
            }

            return date;
        }

        private void PrintPet(Pet pet)
        {
            String sold;
            sold = pet.SoldDate.Year == 0001 ? "No" : pet.SoldDate.ToString();
            
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
                              "Type back to go back to previous menu. \r\n\r\n" +
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
                                      "Type back to go back to previous menu. \r\n\r\n" +
                                      "Input: ");
                    }
                }
                else
                {
                    Console.Write("Invalid Input. Please retry. \r\nInput: ");
                    Console.Write("Type index of pet you wish to interact with.\r\n" +
                                  "Type back to go back to previous menu. \r\n\r\n" +
                                  "Input: ");
                }
            }
            return pet;
        }
    }
}