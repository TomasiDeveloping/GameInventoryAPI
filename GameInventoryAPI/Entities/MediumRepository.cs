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

        public async Task<Medium> GetMediumByNameAsync(string mediumName)
        {
            return await context.Medium.FirstOrDefaultAsync(m => m.Name.Equals(mediumName));
        }

        #endregion

        #region Update Functions
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
        #endregion

        #region Insert Functions
        public async Task<Medium> InsertMediumAsync(Medium medium)
        {
            if (medium == null) return null;

            context.Medium.Add(medium);

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return medium;
        }
        #endregion

        #region Delete Functions
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
        #endregion
    }
}