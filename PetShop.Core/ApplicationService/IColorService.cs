﻿using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IColorService
    {
        Color CreateColor(Color color); // CREATE
        IEnumerable<Color> GetAllColors(); // READ
        Color GetColorById(int id); // READ
        Color UpdateColor(Color color); // UPDATE
        void DeleteColor(int id); // DELETE
    }
}
