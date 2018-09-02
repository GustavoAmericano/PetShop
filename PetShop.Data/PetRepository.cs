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
            return FakeDB._pets;
        }

        public IEnumerable<Pet> GetFiveCheapest()
        {
            return FakeDB._pets.FindAll(x => x.BirthDate.Year == 0001).OrderBy(x => x.Price).Take(5);
        }

        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            if (ascend)
                return FakeDB._pets.OrderBy(x => x.Price);
            else
                return FakeDB._pets.OrderByDescending(x => x.Price);
        }

        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            return FakeDB._pets.FindAll(x => x.PetType.ToLower().Contains(type.ToLower()));
        }

        public IEnumerable<Pet> GetPetsByOwnerId(int id)
        {
            return FakeDB._pets.FindAll(x => x.OwnerId == id);
        }

        public Pet CreatePet(Pet pet)
        {
            pet.Id = ++FakeDB._petId;
            FakeDB._pets.Add(pet);
            return pet;
        }

        public void DeletePet(Pet pet)
        {
            FakeDB._pets.Remove(pet);
        }


        public void SavePet(Pet pet)
        {
            FakeDB._pets[FakeDB._pets.FindIndex(x => x.Id == pet.Id)] = pet;
        }
    }
}