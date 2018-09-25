using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorString { get; set; }
        public List<PetColor> Pets {get; set;}
        
    }
}
