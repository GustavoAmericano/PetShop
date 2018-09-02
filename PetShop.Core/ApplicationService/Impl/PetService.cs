using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
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
            if (pet.Color.Equals("Blyat"))
            {
                throw new InvalidOperationException("Color cannot be blyat.");
            }

            return _petRepository.CreatePet(pet);
        }

        public void DeletePet(Pet pet)
        {
            _petRepository.DeletePet(pet);
        }

        public void SavePet(Pet newPet)
        {
            _petRepository.SavePet(newPet);
        }
    }
}