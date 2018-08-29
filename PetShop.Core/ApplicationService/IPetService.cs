using System.Collections;
using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        Pet CreatePet(Pet pet);
    }
}