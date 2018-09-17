using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop.PetUI
{
    public class PetUI
    {
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;
        private bool _isRunning;

        public PetUI(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
        }

        public void ShowMenu()
        {
            _isRunning = true;
            while (_isRunning)
            {
                Console.Clear();
                Console.WriteLine("\u001b[32m1.\u001b[0m Show all pets\r\n" +
                                  "\u001b[32m2.\u001b[0m Search pets\r\n" +
                                  "\u001b[32m3.\u001b[0m Create new pet" +
                                  "\r\n\r\nType \u001b[32mindex\u001b[0m of the action" +
                                  "\r\nType \u001b[31mback\u001b[0m to return to main menu.\r\n");
                Console.Write("Input: ");
                var input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
                        LoadAllPets();
                        break;
                    case "2":
                        SearchPets();
                        break;
                    case "3":
                        CreatePet();
                        break;
                    case "back":
                        _isRunning = false;
                        break;
                    default:
                        Console.Write("Unrecognized input. Press any key and try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }



        private void SearchPets()
        {
            Console.Clear();
            Console.WriteLine(
                "\u001b[32m1.\u001b[0m Search pets by type\r\n" +
                "\u001b[32m2.\u001b[0m Show pets sorted by price\r\n" +
                "\u001b[32m3.\u001b[0m Show 5 cheapest pets\r\n" +
                "\r\n\r\n" +
                "\r\n\r\nType \u001b[32mindex\u001b[0m of the action" +
                "\r\nType \u001b[31mback\u001b[0m to return to previous menu\r\n"
            );
            Console.Write("Input: ");
            string input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "1":
                    SearchPetByType();
                    break;
                case "2":
                    ShowPetsPriceSort();
                    break;
                case "3":
                    GetFiveCheapest();
                    break;
                case "back":
                    return;
            }
        }

        private void ShowPetsPriceSort()
        {


            Console.Clear();
            Console.Write("\u001b[32m1.\u001b[0m Cheapest -> Highest" +
                          "\r\n\u001b[32m2.\u001b[0m Highest -> Cheapest" +
                          "\r\nType \u001b[31mback\u001b[0m to return to previous menu\r\nInput:");
            string input = Console.ReadLine();
            while ((!input.Equals("1") && !input.Equals("2")))
            {
                Console.Write("Invalid input.\r\nType 1 for ascending order, or 2 for descending order." +
                              " \r\nType \u001b[31mback\u001b[0m to return to previous menu\r\nInput:");
                input = Console.ReadLine();
            }
            switch (input.ToLower())
            {
                case "back":
                    return;
                case "1":
                    while (InteractWithPet(_petService.GetPetsPriceSort(true).ToList())) ;
                    break;
                case "2":
                    while (InteractWithPet(_petService.GetPetsPriceSort(false).ToList())) ;
                    break;
            }
        }

        private void GetFiveCheapest()
        {
            while (InteractWithPet(_petService.GetFiveCheapest().ToList())) ;
        }

        private void LoadAllPets()
        {
            while (InteractWithPet(_petService.GetAllPets().ToList())) ;
        }

        private void SearchPetByType()
        {
            Console.Clear();
            Console.Write("Enter type or part of type. \r\nInput:");
            string input = Console.ReadLine();
            while (InteractWithPet(_petService.SearchPetsByType(input).ToList())) ;
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
            if (pet == null) return false;
            Console.Clear();
            PrintPet(pet);
            Console.Write("\r\n\u001b[32m1.\u001b[0m Edit pet" +
                          "\r\n\u001b[32m2.\u001b[0m Delete pet" +
                          "\r\n\r\nType the \u001b[32mindex\u001b[0m of the action" +
                          "\r\nType \u001b[31mback\u001b[0m to return to previous menu." +
                          "\r\n\r\nInput: "
                          );
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "back":
                    return true;
                case "1":
                    EditPet(pet);
                    break;
                case "2":
                    //_petService.DeletePet(pet);
                    break;
                default:
                    return true;
            }
            return true;
        }

        private void EditPet(Pet pet)
        {
            Pet localPet = new Pet(pet);
            Owner owner = _ownerService.GetOwnerById(pet.OwnerId);
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.Write(
                    "\r\n\u001b[32m1.\u001b[0m Name: " + localPet.Name +
                    "\r\n\u001b[32m2.\u001b[0m Type: " + localPet.PetType +
                    "\r\n\u001b[32m3.\u001b[0m Color: " + localPet.Color +
                    "\r\n\u001b[32m4.\u001b[0m Date of Birth: " + localPet.BirthDate +
                    "\r\n\u001b[32m5.\u001b[0m Sold : " + localPet.SoldDate +
                    "\r\n\u001b[32m6.\u001b[0m Price: " + localPet.Price +
                    "\r\n\u001b[32m7.\u001b[0m Owner: " + owner.FirstName + " "  + owner.LastName
                                                        + " (" + owner.Id + ")" +
                    "\r\n\r\nType \u001b[32mindex\u001b[0m of information you want to change" +
                    "\r\nType  \u001b[31mend\u001b[0m to stop editing" +
                    "\r\nInput: ");
                string input = Console.ReadLine();

                switch (input.ToLower())
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
                    case "7":
                        owner = GetNewOwner();
                        localPet.OwnerId = owner.Id;
                        break;
                }
            }

            if (pet.Equals(localPet)) return;
            Console.Clear();
            Console.Write("Changes were made to the pet." +
                          "\r\nDo you wish to save the changes?(Y/N)" +
                          "\r\nInput:");
            string input1 = Console.ReadLine();
            if (input1.ToLower().Equals("y") || input1.ToLower().Equals("n"))
            {
                //if (input1.ToLower().Equals("y"))
                //    _petService.SavePet(localPet); @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                //else return;
            }
        }

        private Owner GetNewOwner()
        {
            _ownerService.GetAllOwners().OrderBy(x => x.Id).ToList().ForEach(x =>
            {
                Console.WriteLine(
                    "\u001b[32m" + x.Id + ".\u001b[0m " + x.FirstName + " " + x.LastName +
                    ", " + x.Address + ", " + x.PhoneNumber + ", " + x.Email + ", "
                );
            });
            int id;
            string idInput = "";
            while (true)
            {
                Console.Write("Type \u001b[32mindex\u001b[0m of new owner: ");
                idInput = Console.ReadLine();

                if (int.TryParse(idInput, out id))
                {
                    Owner newOwner = _ownerService.GetOwnerById(id);
                    if (newOwner != null)
                    {
                        return newOwner;
                    }
                    break;
                }
                Console.WriteLine("Invalid input. Either index of owner was wrong, or you did not enter one. Press any key and try again.");
                Console.ReadKey();
            }

            return null; // Cannot reach, so who cares
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
            Owner owner = GetNewOwner();
            int ownerId = owner.Id;

            _petService.CreatePet(new Pet()
            {
                Name = name,
                PetType = type,
                Color = color,
                BirthDate = dob,
                Price = price,
                OwnerId = ownerId,
            });
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
            var sold = pet.SoldDate.Year == 0001 ? "No" : pet.SoldDate.ToString();
            Owner owner = _ownerService.GetOwnerById(pet.OwnerId);
            Console.WriteLine(
                //"Name: " + pet.Name + "  (\u001b[32m" + pet.Id + "\u001b[0m)" +
                "_____________" + pet.Name + "_____________" +
                "\r\nID:\u001b[32m " + pet.Id + "\u001b[0m" +
                "\r\nType: " + pet.PetType +
                "\r\nColor: " + pet.Color +
                "\r\nDOB: " + pet.BirthDate +
                "\r\nSold: " + sold +
                "\r\nPrice: " + pet.Price +
                "\r\n Owner: " + owner.FirstName + " " + owner.LastName + " (" + owner.Id + ")"
            );
        }

        private Pet SelectPet(IEnumerable<Pet> pets)
        {
            Pet pet = null;
            Console.Write("Type \u001b[32mID\u001b[0m of pet you wish to interact with.\r\n" +
                              "Type \u001b[31mback\u001b[0m to go back to previous menu. \r\n\r\n" +
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
                    }
                }
                else
                {
                    Console.Write("Invalid Input. Please retry. \r\nInput: ");
                }
            }
            return pet;
        }
    }
}