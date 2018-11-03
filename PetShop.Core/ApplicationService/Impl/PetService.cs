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
            try
            {
                return _petRepository.GetAllPets();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Pet> GetFiveCheapest()
        {
            try
            {
                return _petRepository.GetFiveCheapest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            try
            {
                return _petRepository.GetPetsPriceSort(ascend);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            try
            {
                return _petRepository.SearchPetsByType(type);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Pet> GetPetsByOwnerId(int id)
        {
            try
            {
                return _petRepository.GetPetsByOwnerId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
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

            try
            {
                return _petRepository.CreatePet(pet);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void DeletePet(int id)
        {
            try
            {
                _petRepository.DeletePet(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Pet SavePet(int id, Pet newPet)
        {
            if (id != newPet.Id) throw new ArgumentException("Id does not match!");
            try
            {
                return _petRepository.SavePet(id, newPet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Pet GetPetById(int id)
        {
            try
            {
                return _petRepository.GetPetById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}