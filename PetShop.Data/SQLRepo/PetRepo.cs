using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Adds pet to DB.
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>Pet with id from DB.</returns>
        public Pet CreatePet(Pet pet)
        {
            // If owner exists in DB, attach rather than creating a new.
            if(_ctx.Owners.FirstOrDefault(x => x.Id == pet.Owner.Id) != null)
                _ctx.Owners.Attach(pet.Owner);
            _ctx.Pets.Add(pet);
            _ctx.SaveChanges(); // actually execute the queries
            return pet;
        }

        /// <summary>
        /// Deletes specific pet from DB.
        /// </summary>
        /// <param name="id"></param>
        public void DeletePet(int id)
        {
            _ctx.Pets.Remove(_ctx.Pets.First(x => x.Id == id));
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Gets all pets from the DB
        /// </summary>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> GetAllPets()
        {
            return _ctx.Pets;
        }

        /// <summary>
        /// Gets five cheapest, available pets from DB.
        /// </summary>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> GetFiveCheapest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a specific pet.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Pet</returns>
        public Pet GetPetById(int id)
        {
            Pet pet =  _ctx.Pets.Include(x => x.Owner).FirstOrDefault(x => x.Id == id);
            return pet;
        }

        /// <summary>
        /// Gets a list of pets based on their owners id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IEnumeranble of Pets.</returns>
        public IEnumerable<Pet> GetPetsByOwnerId(int id)
        {
            //return _ctx.Pets.Where(x => x.Owner.Id == id);
            return _ctx.Pets.Include(x => x.Owner).Where(o => o.Id == id);
        }

        /// <summary>
        /// Gets pets in a list sorted by their price.
        /// </summary>
        /// <param name="ascend"></param>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> GetPetsPriceSort(bool ascend)
        {
            return _ctx.Pets.OrderBy(x => x.Price);
        }

        /// <summary>
        /// Updates a pet in the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPet"></param>
        public void SavePet(int id, Pet newPet)
        {
            _ctx.Update(newPet);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Gets a list of pets with a specific type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            return _ctx.Pets.Where(x => x.PetType.Equals(type));
        }
    }
}
