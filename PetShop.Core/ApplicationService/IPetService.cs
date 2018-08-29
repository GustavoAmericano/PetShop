﻿using System.Collections;
using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        IEnumerable<Pet> GetFiveCheapest();
        IEnumerable<Pet> GetPetsPriceSort(bool ascend);

        Pet CreatePet(Pet pet);
        void DeletePet(Pet pet);
    }
}