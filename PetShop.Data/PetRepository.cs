using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Data
{
    public class PetRepository : IPetRepository
    {
        public PetRepository()
        {

        }

        public IEnumerable<Pet> GetAllPets()
        {
            return FakeDB._Pets;
        }

        public Pet CreatePet(Pet pet)
        {
            pet.Id = ++FakeDB._Id;
            FakeDB._Pets.Add(pet);
            return pet;
        }
    }
}