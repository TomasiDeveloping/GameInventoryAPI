using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class GenreRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Genres>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<Genres> GetGenreByIdAsync(int id)
        {
            return await context.Genres.FindAsync(id);
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