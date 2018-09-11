﻿using System;
using System.Collections.Generic;

namespace PetShop.Core.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Pet> Pets { get; set; }
    }
}