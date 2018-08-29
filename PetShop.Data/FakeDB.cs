using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.Entities;

namespace PetShop.Data
{
    public static class FakeDB
    {
        public static List<Pet> _Pets;
        public static int _Id;

        public static void InitData()
        {
           _Pets = new List<Pet>
           {
               new Pet()
               {
                   Id = 0,
                   Name = "Bob",
                   PetType = "Cat",
                   Color = "Cyan blue",
                   BirthDate = DateTime.MinValue.Date,
                   Price = 0.001,
               },
               new Pet()
               {
                   Id = 1,
                   Name = "King",
                   PetType = "Dog",
                   Color = "maroon Red",
                   BirthDate = DateTime.Today,
                   Price = 150,
               },
               new Pet()
               {
                   Id = 2,
                   Name = "Linux",
                   PetType = "Penguin",
                   Color = "Black & White",
                   BirthDate = DateTime.MinValue.Date,
                   Price = 1337,
               },
               new Pet()
               {
                   Id = 3,
                   Name = "Buster",
                   PetType = "Dog",
                   Color = "Black",
                   BirthDate = DateTime.MinValue.Date,
                   Price = 125.89,
               },
           };
            _Id = _Pets.Last().Id;
        }
    }
}