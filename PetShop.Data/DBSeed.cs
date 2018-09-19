﻿using System;
using PetShop.Core.Entities;

namespace PetShop.Data
{
    public class DBSeed
    {
        public static void SeedDB(PetShopContext ctx)
        {
            ctx.Database.EnsureDeleted(); // Delete ENTIRE Db!
            ctx.Database.EnsureCreated(); // Recreate Db

            var owner1 = ctx.Owners.Add(new Owner()
            {
                FirstName = "Ludvig",
                LastName = "Hansen",
                Address = "Somestreet 123",
                Email = "LV123@provider.com",
                PhoneNumber = "+45 12 34 56 78"
            }).Entity;
            var owner2 = ctx.Owners.Add(new Owner()
            {
                FirstName = "Henrik",
                LastName = "Larsen",
                Address = "Somestreet 431",
                Email = "321HL@provider.com",
                PhoneNumber = "+45 87 65 43 21"
            }).Entity;

            var pet1 = ctx.Pets.Add(new Pet()
            {
                Name = "Buster",
                PetType = "Dog",
                Color = "Black",
                BirthDate = DateTime.Now.AddYears(-2).AddMonths(4).AddDays(-23),
                SoldDate = DateTime.MinValue,
                Price = 2500,
                Owner = owner1
            }).Entity;

            ctx.SaveChanges();
        }
    }
}