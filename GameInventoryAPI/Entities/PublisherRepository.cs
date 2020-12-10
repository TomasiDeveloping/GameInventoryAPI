using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class PublisherRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Publishers>> GetPublishersAsync()
        {
            return await context.Publishers.ToListAsync();
        }

        public async Task<Publishers> GetPublisherByIdAsync(int id)
        {
            return await context.Publishers.FindAsync(id);
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