using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IColorService
    {
        IEnumerable<Color> GetAllcolors();
    }
}
