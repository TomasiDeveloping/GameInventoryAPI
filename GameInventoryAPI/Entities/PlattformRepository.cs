using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class PlattformRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Plattform>> GetPlattformsAsync()
        {
            return await context.Plattform.ToListAsync();
        }

        public async Task<Plattform> GetPlattformByIdAsync(int id)
        {
            return await context.Plattform.FindAsync(id);
        }
    }
}