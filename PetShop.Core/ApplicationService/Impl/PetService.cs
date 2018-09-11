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

        public Pet GetExtendedPet(int id)
        {
            Pet pet = GetPetById(id);
            if (pet == null) return null;
            pet.Owner = _ownerRepository.GetOwnerById(pet.Owner.Id);
            return pet;
        }


        public Pet CreatePet(Pet pet)
        {
            if (pet.Color.Equals("Blyat"))
            {
                throw new InvalidOperationException("Color cannot be blyat.");
            }

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