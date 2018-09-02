using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAllOwners();

        Owner CreateOwner(Owner owner);
        Owner GetOwnerById(int id);

    }
}