using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Data.SQLRepo
{
    public class PetRepo : IPetRepository
    {
        PetShopContext _ctx;

        public PetRepo(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public Pet CreatePet(Pet pet)
        {
            //_ctx.Owners.Attach(pet.Owner);
            pet = _ctx.Pets.Add(pet).Entity;
            _ctx.SaveChanges();
            return pet;

        }

        public void DeletePet(int id)
        {
            _ctx.Pets.Remove(_ctx.Pets.First(x => x.Id == id));
            _ctx.SaveChanges();
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _ctx.Pets;
        }

        public IEnumerable<Pet> GetFiveCheapest()
        {
            throw new NotImplementedException();
        }

        public Pet GetPetById(int id)
        {
            Pet pet =  _ctx.Pets.FirstOrDefault(x => x.Id == id);
            return pet;
        }

        public IEnumerable<Pet> GetPetsByOwnerId(int id)
        {
            return _ctx.Pets.Where(x => x.OwnerId == id);
        }

        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            return _ctx.Pets.OrderBy(x => x.Price);
        }

        public void SavePet(int id, Pet newPet)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            return _ctx.Pets.Where(x => x.PetType.Equals(type));
        }
    }
}
