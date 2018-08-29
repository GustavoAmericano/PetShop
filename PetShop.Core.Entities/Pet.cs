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


        public override string ToString()
        {
            return
                "Name: " + Name +
                "\r\nId: " + Id +
                "\r\nType: " + PetType +
                "\r\nColor: " + Color +
                "\r\nDOB: " + BirthDate +
                "\r\nSold: " + SoldDate +
                "\r\nPrice: " + Price;
        }
    }
}
