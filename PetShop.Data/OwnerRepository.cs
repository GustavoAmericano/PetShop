using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Data
{
    public class OwnerRepository : IOwnerRepository
    {

        public IEnumerable<Owner> GetAllOwners()
        {
            return FakeDB._owners;
        }

        public Owner CreateOwner(Owner owner)
        {
            owner.Id = ++FakeDB._ownerId;
            FakeDB._owners.Add(owner);
            return owner;
        }

        public Owner GetOwnerById(int id)
        {
            return FakeDB._owners.First(x => x.Id == id);
        }
    }
}