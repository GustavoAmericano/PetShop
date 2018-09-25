using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService.Impl
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository colorRepo;

        public ColorService(IColorRepository colorRepo)
        {
            this.colorRepo = colorRepo;
        }

        public Color CreateColor(Color color)
        {
            return colorRepo.CreateColor(color);
        }

        public IEnumerable<Color> GetAllColors()
        {
            return colorRepo.GetAllColors();
        }

        public Color GetColorById(int id)
        {
            return colorRepo.GetColorById(id);
        }

        public Color UpdateColor(Color color)
        {
            return colorRepo.UpdateColor(color);
        }

        public void DeleteColor(int id)
        {
            colorRepo.DeleteColor(id);
        }
    }
}