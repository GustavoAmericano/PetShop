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
            if(FakeDB._owners.Exists(x => x.Id == id))
            {
                return FakeDB._owners.First(x => x.Id == id);

            }
            return null;
        }

        public void SaveOwner(int id, Owner owner)
        {
            owner.Id = id;
            FakeDB._owners[FakeDB._owners.FindIndex(x => x.Id == id)] = owner;
        }

        public void DeleteOwner(int id)
        {
            if(FakeDB._owners.Exists(x  => x.Id == id))
            {
                FakeDB._owners.Remove(FakeDB._owners.Find(x => x.Id == id));
            }
        }
    }
}