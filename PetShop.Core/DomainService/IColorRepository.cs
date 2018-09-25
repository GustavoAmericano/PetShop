using System.Collections.Generic;
using PetShop.Core.Entities;

namespace PetShop.Core.DomainService
{
    public interface IColorRepository
    {
        Color CreateColor(Color color); // CREATE
        IEnumerable<Color> GetAllColors(); // READ
        Color GetColorById(int id); // READ
        Color UpdateColor(Color color); // UPDATE
        void DeleteColor(int id); // DELETE
    }
}