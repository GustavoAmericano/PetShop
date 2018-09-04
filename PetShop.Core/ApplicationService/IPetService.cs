using System.Collections;
using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        IEnumerable<Pet> GetFiveCheapest();
        IEnumerable<Pet> GetPetsPriceSort(bool ascend);
        IEnumerable<Pet> SearchPetsByType(string type);
        IEnumerable<Pet> GetPetsByOwnerId(int id);

        Pet CreatePet(Pet pet);
        void DeletePet(int id);
        void SavePet(int id, Pet newPet);

        Pet GetPetById(int id);

    }
}