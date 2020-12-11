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
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Plattform>> GetPlattformsAsync()
        {
            return await context.Plattform.ToListAsync();
        }

        public async Task<Plattform> GetPlattformByIdAsync(int id)
        {
            return await context.Plattform.FindAsync(id);
        }

        #endregion

        #region Update Functions
        public async Task<Plattform> UpdatePlattformAsync(Plattform plattform)
        {
            if (plattform == null) return null;

            var plattformToUpdate = await context.Plattform.FindAsync(plattform.PlattformId);
            if (plattformToUpdate == null) return null;

            plattformToUpdate.PlattformId = plattform.PlattformId;
            plattformToUpdate.Name = plattform.Name;
            plattformToUpdate.Producer = plattform.Producer;
            plattformToUpdate.Release = plattform.Release;
            plattformToUpdate.Description = plattform.Description;

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return plattform;
        }
        #endregion

        #region Insert Functions
        public async Task<Plattform> InsertPlattformAsync(Plattform plattform)
        {
            if (plattform == null) return null;

            context.Plattform.Add(plattform);
            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return plattform;
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeletePlattformByIdAsync(int plattformId)
        {
            if (plattformId <= 0) return false;

            var plattformToDelete = await context.Plattform.FindAsync(plattformId);
            if (plattformToDelete == null) return false;

            context.Plattform.Remove(plattformToDelete);

            return await context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}