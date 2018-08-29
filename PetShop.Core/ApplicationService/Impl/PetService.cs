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

        public Pet CreatePet(Pet pet)
        {
            if (pet.Color.Equals("Blyat"))
            {
                throw new InvalidOperationException("Color cannot be blyat.");
            }

            return _petRepository.CreatePet(pet);
        }
    }
}