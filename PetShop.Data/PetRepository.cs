using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Pet> GetFiveCheapest()
        {
            int last = 5;
            if (FakeDB._Pets.Count < 5)
            {
                last = FakeDB._Pets.Count;
            }
            return FakeDB._Pets.OrderBy(x => x.Price).ToList().GetRange(0, last);
        }

        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            if (ascend)
                return FakeDB._Pets.OrderBy(x => x.Price);
            else
                return FakeDB._Pets.OrderByDescending(x => x.Price);
        }

        public Pet CreatePet(Pet pet)
        {
            pet.Id = ++FakeDB._Id;
            FakeDB._Pets.Add(pet);
            return pet;
        }

        public void DeletePet(Pet pet)
        {
            FakeDB._Pets.Remove(pet);
        }
    }
}