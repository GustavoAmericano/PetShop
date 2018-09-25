using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Data.SQLRepo
{
    public class ColorRepo  : IColorRepository
    {
        private readonly PetShopContext _ctx;
        
        public ColorRepo(PetShopContext ctx)
        {
            this._ctx = ctx;
        }

        public Color CreateColor(Color color)
        {
            _ctx.Colors.Attach(color).State = EntityState.Added;
            _ctx.SaveChanges();
            return color;
        }

        public IEnumerable<Color> GetAllColors()
        {
            return _ctx.Colors;
        }

        public Color GetColorById(int id)
        {
            return _ctx.Colors
                .Include(x => x.Pets)
                .ThenInclude(x => x.Pet)
                .FirstOrDefault(x => x.Id == id);
        }

        public Color UpdateColor(Color color)
        {
            _ctx.Colors.Attach(color).State = EntityState.Modified;
            _ctx.Entry(color).Collection(x => x.Pets).IsModified = true;
            _ctx.SaveChanges();
            return color;
        }

        public void DeleteColor(int id)
        {
            _ctx.Colors.Remove(_ctx.Colors.First(x => x.Id == id));
        }
    }
}