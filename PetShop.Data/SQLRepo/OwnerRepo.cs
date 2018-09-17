using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Owner CreateOwner(Owner owner)
        {
            owner = _ctx.Owners.Add(owner).Entity;
            _ctx.SaveChanges();
            return owner;
        }

        public void DeleteOwner(int id)
        {
            _ctx.Remove(_ctx.Owners.First(x => x.Id == id));
            _ctx.SaveChanges();
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _ctx.Owners;
        }

        public Owner GetOwnerById(int id)
        {
            return _ctx.Owners.FirstOrDefault(x => x.Id == id);
        }

        public void SaveOwner(int id, Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
