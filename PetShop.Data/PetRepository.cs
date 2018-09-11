using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            return FakeDB.pets;
        }

        public IEnumerable<Pet> GetFiveCheapest()
        {
            return FakeDB.pets.FindAll(x => x.BirthDate.Year == 0001).OrderBy(x => x.Price).Take(5);
        }

        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            if (ascend)
                return FakeDB.pets.OrderBy(x => x.Price);
            else
                return FakeDB.pets.OrderByDescending(x => x.Price);
        }

        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            return FakeDB.pets.FindAll(x => x.PetType.ToLower().Contains(type.ToLower()));
        }

        public IEnumerable<Pet> GetPetsByOwnerId(int id)
        {
            if (FakeDB.pets.Exists(x => x.Owner.Id == id))
            {
                return FakeDB.pets.FindAll(x => x.Owner.Id == id);
            }
            return null;
        }

        public Pet CreatePet(Pet pet)
        {
            pet.Id = ++FakeDB.petId;
            FakeDB.pets.Add(pet);
            return pet;
        }

        public void DeletePet(int id)
        {
            FakeDB.pets.Remove(FakeDB.pets.Find(x => x.Id == id));
        }


        public void SavePet(int id, Pet pet)
        {
            pet.Id = id;
            FakeDB.pets[FakeDB.pets.FindIndex(x => x.Id == id)] = pet;
        }

        public Pet GetPetById(int id)
        {
            return FakeDB.pets.Select(x => new Pet()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
                PetType = x.PetType,
                Price = x.Price,
                BirthDate = x.BirthDate,
                SoldDate = x.SoldDate,
                Owner = x.Owner
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}