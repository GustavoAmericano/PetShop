using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService
{
    public interface IOwnerService
    {
        IEnumerable<Owner> GetAllOwners();
        Owner CreateOwner(Owner owner);
        Owner GetOwnerById(int id);
    }
}