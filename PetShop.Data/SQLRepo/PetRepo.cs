
using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //var changeTracker = _ctx.ChangeTracker.Entries<Owner>();
            try
            {
                _ctx.Attach(pet).State = EntityState.Added;
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create pet!");
            }

            return pet;
        }

        /// <summary>
        /// Gets all pets from the DB
        /// </summary>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> GetAllPets()
        {
            try
            {
                return _ctx.Pets;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load pets from Database!");
            }
        }

        /// <summary>
        /// Gets a specific pet.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Pet</returns>
        public Pet GetPetById(int id)
        {
            Pet pet = null;
            try
            {
                 pet = _ctx.Pets
                    .Include(x => x.Owner)
                    .Include(p => p.Colors)
                    .ThenInclude(p => p.Color)
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not load pet with ID {id}. Does it exist?");
            }
            return pet;

        }

        /// <summary>
        /// Updates a pet in the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPet"></param>
        public Pet SavePet(int id, Pet newPet)
        {
            //if(newPet.Owner != null)
            //{
            //    newPet.Owner = _ctx.Owners.FirstOrDefault(x => x.Id == newPet.Owner.Id);
            //}
            //else
            //{
            //    _ctx.Entry(newPet).Reference(x => x.Owner).IsModified = true;
            //}
            //_ctx.Update(newPet);
            if(!_ctx.Pets.Any(x => x.Id == id)) throw new ArgumentException($"Could not find any pet with ID {id}!");
            try
            {
                _ctx.Attach(newPet).State = EntityState.Modified;
                _ctx.Entry(newPet).Reference(x => x.Owner).IsModified = true;
                _ctx.Entry(newPet).Collection(np => np.Colors).IsModified = true;
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to save pet!");
            }
            return newPet;
        }

        /// <summary>
        /// Deletes specific pet from DB.
        /// </summary>
        /// <param name="id"></param>
        public void DeletePet(int id)
        {
            if(!_ctx.Pets.Any(x => x.Id == id))
                throw new ArgumentException($"No pet with ID {id} was found!");
            try
            {
                _ctx.PetColors.RemoveRange(_ctx.PetColors.Where(x => x.PetId == id));
                _ctx.Pets.Remove(_ctx.Pets.First(x => x.Id == id));
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to delete pet!");
            }
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
        /// Gets a list of pets with a specific type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> SearchPetsByType(string type)
        {
            return _ctx.Pets.Where(x => x.PetType.Equals(type));
        }

        /// <summary>
        /// Gets five cheapest, available pets from DB. NOT YET IMPLEMENTED
        /// </summary>
        /// <returns>IEnumerable of Pets.</returns>
        public IEnumerable<Pet> GetFiveCheapest()
        {
            throw new NotImplementedException();
        }
    }
}
