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
            return FakeDB.owners;
        }

        public Owner CreateOwner(Owner owner)
        {
            owner.Id = ++FakeDB.ownerId;
            FakeDB.owners.Add(owner);
            return owner;
        }

        public Owner GetOwnerById(int id)
        {
            if(FakeDB.owners.Exists(x => x.Id == id))
            {
                return FakeDB.owners.
                    Select(x => new Owner()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName, 
                        Address =  x.Address,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email
                    }).
                    FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        public void SaveOwner(int id, Owner owner)
        {
            owner.Id = id;
            FakeDB.owners[FakeDB.owners.FindIndex(x => x.Id == id)] = owner;
        }

        public void DeleteOwner(int id)
        {
            if(FakeDB.owners.Exists(x  => x.Id == id))
            {
                FakeDB.owners.Remove(FakeDB.owners.Find(x => x.Id == id));
            }
        }
    }
}