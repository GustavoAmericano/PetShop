using System.Collections;
using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> GetAllPets();
        Pet CreatePet(Pet pet);
    }

    
}