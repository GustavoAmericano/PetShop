using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PetShop.Data.SQLRepo
{
    public class OwnerRepo : IOwnerRepository
    {
        PetShopContext _ctx;

        public OwnerRepo(PetShopContext ctx)
        {
            _ctx = ctx;
        }


        /// <summary>
        /// Adds the owner to the DB.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>Owner with id given by DB.</returns>
        public Owner CreateOwner(Owner owner)
        {
            owner = _ctx.Owners.Add(owner).Entity;
            _ctx.SaveChanges();
            return owner;
        }

        /// <summary>
        /// Deletes the specific owner from the DB.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOwner(int id)
        {
            //var pets = _ctx.Pets.Where(x => x.Owner.Id == id);
            //pets.ToList().ForEach(x =>
            //{
            //    x.Owner = null;
            //    _ctx.Update(x);
            //});
            
            _ctx.Remove(_ctx.Owners.First(x => x.Id == id));
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Gets all owners from DB.
        /// </summary>
        /// <returns>An IEnumerable of Owners</returns>
        public IEnumerable<Owner> GetAllOwners()
        {
            return _ctx.Owners;
        }

        /// <summary>
        /// Gets a specific Owner from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An Owner</returns>
        public Owner GetOwnerById(int id)
        {
            return _ctx.Owners.Include(x => x.Pets).FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Updates an Owner in the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        public void SaveOwner(int id, Owner owner)
        {
            _ctx.Update(owner);
            _ctx.SaveChanges();
        }
    }
}
