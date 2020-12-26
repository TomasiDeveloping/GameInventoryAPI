using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class PlattformRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        #region Get Functions

        /// <summary>
        ///  Get all Platforms Async
        /// </summary>
        /// <returns>IEnumerable of Plattform</returns>
        public async Task<IEnumerable<Plattform>> GetPlattformsAsync()
        {
            return await context.Plattform.ToListAsync();
        }

        /// <summary>
        /// Get a Platform by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Plattform</returns>
        public async Task<Plattform> GetPlattformByIdAsync(int id)
        {
            return await context.Plattform.FindAsync(id);
        }

        /// <summary>
        /// Get a Platform by Name Async
        /// </summary>
        /// <param name="plattformName"></param>
        /// <returns>Plattform</returns>
        public async Task<Plattform> GetPlattformByNameAsync(string plattformName)
        {
            return await context.Plattform.FirstOrDefaultAsync(p => p.Name.Equals(plattformName));
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a Platform Async
        /// </summary>
        /// <param name="plattform"></param>
        /// <returns>Plattform</returns>
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
            plattformToUpdate.PhotoUrl = plattform.PhotoUrl;

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return plattform;
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new Platform to the DB Async
        /// </summary>
        /// <param name="plattform"></param>
        /// <returns>Plattform</returns>
        public async Task<Plattform> InsertPlattformAsync(Plattform plattform)
        {
            if (plattform == null) return null;

            context.Plattform.Add(plattform);
            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return plattform;
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Platform from the DB by Id Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeletePlattformByIdAsync(int plattformId)
        {
            if (plattformId <= 0) return false;
            var checkPlattformInGames = await context.Game_Plattform.Where(p => p.PlattformId == plattformId).ToListAsync();
            if (checkPlattformInGames.Count() != 0) throw new ArgumentException("Diese Plattform wird in Games verwendet und kann nicht gelöscht werden");

            var plattformToDelete = await context.Plattform.FindAsync(plattformId);
            if (plattformToDelete == null) return false;

            context.Plattform.Remove(plattformToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a Platform from the DB by Id with all dependencies Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeletePlattformByIdAsync(int plattformId)
        {
            var plattformToDelete = await context.Plattform.FindAsync(plattformId);
            if (plattformToDelete == null) return false;

            var gamesWhitPlattform = await context.Game_Plattform.Where(p => p.PlattformId == plattformId).ToListAsync();

            if (gamesWhitPlattform.Count() != 0)
            {
                foreach (var item in gamesWhitPlattform)
                {
                    var plattform = await context.Game_Plattform.FindAsync(item.GamePlattformId);
                    context.Game_Plattform.Remove(plattform);
                }
            }
            context.Plattform.Remove(plattformToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        #endregion Delete Functions
    }
}