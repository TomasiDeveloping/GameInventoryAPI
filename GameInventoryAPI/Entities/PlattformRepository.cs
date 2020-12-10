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
        #endregion

        #region Insert Functions
        #endregion

        #region Delete Functions
        #endregion
    }
}