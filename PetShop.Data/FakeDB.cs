using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.Entities;

namespace PetShop.Data
{
    public static class FakeDB
    {
        public static List<Pet> _pets;
        public static List<Owner> _owners;
        public static int _petId;
        public static int _ownerId;

        public static void InitData()
        {
            Random rnd = new Random();
            _owners = new List<Owner>
            {
                new Owner()
                {
                    Id = -1,
                    FirstName = "NONE",
                },

                new Owner()
                {
                    Id = 0,
                    FirstName = "Ludvig",
                    LastName = "Hansen",
                    Address = "N/A",
                    Email = "LudvigHansen@gmail.com",
                    PhoneNumber = "+45 12 34 56 78"
                },
                new Owner()
                {
                    Id = 149,
                    FirstName = "Erik",
                    LastName = "Svendsen",
                    Address = "Gunnarsvej 43",
                    Email = "ErikSvendsen@gmail.com",
                    PhoneNumber = "+45 12 34 56 78"
                },
                new Owner()
                {
                    Id = 29,
                    FirstName = "Britt",
                    LastName = "Eriksen",
                    Address = "Randersvej 292",
                    Email = "BrittEriksen@gmail.com",
                    PhoneNumber = "+45 88 88 88 88"
                },
            };

            _pets = new List<Pet>
           {
               new Pet()
               {
                   Id = 0,
                   Name = "Bob",
                   PetType = "Cat",
                   Color = "Cyan blue",
                   BirthDate = DateTime.MinValue.Date,
                   Price = 0.001,
                   OwnerId = _owners[rnd.Next(_owners.Count)].Id
               },
               new Pet()
               {
                   Id = 1,
                   Name = "King",
                   PetType = "Dog",
                   Color = "maroon Red",
                   BirthDate = DateTime.Today,
                   Price = 150,
                   OwnerId = _owners[rnd.Next(_owners.Count)].Id
               },
               new Pet()
               {
                   Id = 2,
                   Name = "Linux",
                   PetType = "Penguin",
                   Color = "Black & Yellow (And white)",
                   BirthDate = DateTime.MinValue.Date,
                   Price = 1337,
                   OwnerId = _owners[rnd.Next(_owners.Count)].Id
               },
               new Pet()
               {
                   Id = 3,
                   Name = "Buster",
                   PetType = "Dog",
                   Color = "Black",
                   BirthDate = DateTime.MinValue.Date,
                   Price = 125.89,
                   OwnerId = _owners[rnd.Next(_owners.Count)].Id
               },
           };

            _petId = _pets.OrderBy(x => x.Id).Last().Id;
            _ownerId = _owners.OrderBy(x => x.Id).Last().Id;
        }
    }
}