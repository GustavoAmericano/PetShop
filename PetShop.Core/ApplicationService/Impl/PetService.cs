using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private IOwnerRepository _ownerRepository;

        public PetService(IPetRepository petRepository,
            IOwnerRepository ownerRepository)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _petRepository.GetAllPets();
        }

        public IEnumerable<Pet> GetFiveCheapest()
        {
            return _petRepository.GetFiveCheapest();
        }

        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            return _petRepository.GetPetsPriceSort(ascend);
        }

        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            return _petRepository.SearchPetsByType(type);
        }

        public IEnumerable<Pet> GetPetsByOwnerId(int id)
        {
            return _petRepository.GetPetsByOwnerId(id);
        }

        public Pet CreatePet(Pet pet)
        {
            string illegalVariables = "";
            bool hasFailed = false;

            if(pet.Name == null)
            {
                hasFailed = true;
                illegalVariables += "\nPet's name was null! ";
            }
            if (pet.Colors == null)
            {
                hasFailed = true;
                illegalVariables += "\nPet's color was null! ";
            }
            if (pet.PetType == null)
            {
                hasFailed = true;
                illegalVariables += "\nPet's Type was null! ";
            }
            if (pet.Price == double.MinValue)
            {
                hasFailed = true;
                illegalVariables += "\nPet's price was null!";
            }

            if (hasFailed) throw new ArgumentException($"Following fields failed: {illegalVariables}");

            return _petRepository.CreatePet(pet);
        }

        public void DeletePet(int id)
        {
            _petRepository.DeletePet(id);
        }

        public void SavePet(int id, Pet newPet)
        {
            _petRepository.SavePet(id, newPet);
        }

        public Pet GetPetById(int id)
        {
           return _petRepository.GetPetById(id);
        }
    }
}