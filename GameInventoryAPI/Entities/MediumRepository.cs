using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class MediumRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Medium>> GetMediumsAsync()
        {
            return await context.Medium.ToListAsync();
        }

        public async Task<Medium> GetMediumByIdAsync(int id)
        {
            return await context.Medium.FindAsync(id);
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