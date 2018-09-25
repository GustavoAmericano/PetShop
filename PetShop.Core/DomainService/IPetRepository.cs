using System.Collections;
using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> GetAllPets();
        IEnumerable<Pet> GetFiveCheapest();
        IEnumerable<Pet> GetPetsPriceSort(bool ascend);
        IEnumerable<Pet> SearchPetsByType(string type);
        IEnumerable<Pet> GetPetsByOwnerId(int id);
        Pet GetPetById(int id);

        Pet CreatePet(Pet pet);
        void DeletePet(int id);
        Pet SavePet(int id, Pet newPet);


    }


}