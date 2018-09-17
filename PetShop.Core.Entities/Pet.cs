using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PetType { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public Double Price { get; set; }
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

        public Pet(){}

        public Pet(Pet pet)
        {
            OwnerId = pet.OwnerId;
            Id = pet.Id;
            Name = pet.Name;
            PetType = pet.PetType;
            BirthDate = pet.BirthDate;
            SoldDate = pet.SoldDate;
            Color = pet.Color;
            Price = pet.Price;
            Owner = pet.Owner;
        }

        public override string ToString()
        {
            return "A nice " + PetType + ".";
        }

        //public override bool Equals(object obj)
        //{
        //    Pet pet = (Pet) obj;

        //    if (Id == pet.Id
        //        && Name == pet.Name
        //        && PetType == pet.PetType
        //        && BirthDate == pet.BirthDate
        //        && SoldDate == pet.SoldDate
        //        && Color == pet.Color
        //        && Math.Abs(Price - pet.Price) < 0.02
        //        //&& Owner == pet.Owner
        //        )
        //    {
        //        return true;
        //    }

        //    return false;

        //}
    }
}
