using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class MediumRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        #region Get Functions

        /// <summary>
        /// Get all Mediums Async
        /// </summary>
        /// <returns>IEnumerable of Medium</returns>
        public async Task<IEnumerable<Medium>> GetMediumsAsync()
        {
            return await context.Medium.ToListAsync();
        }

        /// <summary>
        /// Get a Medium by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Medium</returns>
        public async Task<Medium> GetMediumByIdAsync(int id)
        {
            return await context.Medium.FindAsync(id);
        }

        /// <summary>
        /// Get a Medium by Name Async
        /// </summary>
        /// <param name="mediumName"></param>
        /// <returns>Medium</returns>
        public async Task<Medium> GetMediumByNameAsync(string mediumName)
        {
            return await context.Medium.FirstOrDefaultAsync(m => m.Name.Equals(mediumName));
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a Medium Async
        /// </summary>
        /// <param name="medium"></param>
        /// <returns>Medium</returns>
        public async Task<Medium> UpdateMediumAsync(Medium medium)
        {
            if (medium == null) return null;

            var mediumToUpdate = await context.Medium.FindAsync(medium.MediumId);
            if (mediumToUpdate == null) return null;

            mediumToUpdate.MediumId = medium.MediumId;
            mediumToUpdate.Name = medium.Name;

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return mediumToUpdate;
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new Medium to the DB Async
        /// </summary>
        /// <param name="medium"></param>
        /// <returns>Medium</returns>
        public async Task<Medium> InsertMediumAsync(Medium medium)
        {
            if (medium == null) return null;

            context.Medium.Add(medium);

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return medium;
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Medium from the DB by Id Async
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteMediumByIdAsync(int mediumId)
        {
            if (mediumId <= 0) return false;
            var checkMediumInGames = await context.Game_Medium.Where(m => m.MediumId == mediumId).ToListAsync();
            if (checkMediumInGames.Count() != 0) throw new ArgumentException("Dieser Mediumtyp wird in Games verwendet und kann nicht gelöscht werden");

            var mediumToDelete = await context.Medium.FindAsync(mediumId);
            if (mediumToDelete == null) return false;

            context.Medium.Remove(mediumToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a Medium from the DB by Id with all dependencies Async
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeleteMediumByIdAsync(int mediumId)
        {
            var mediumToDelete = await context.Medium.FindAsync(mediumId);
            if (mediumToDelete == null) return false;

            var gamesWhitMedium = await context.Game_Medium.Where(m => m.MediumId == mediumId).ToListAsync();
            if (gamesWhitMedium.Count() != 0)
            {
                foreach (var item in gamesWhitMedium)
                {
                    var medium = await context.Game_Medium.FindAsync(item.GameMediumId);
                    context.Game_Medium.Remove(medium);
                }
            }
            context.Medium.Remove(mediumToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        #endregion Delete Functions
    }
}