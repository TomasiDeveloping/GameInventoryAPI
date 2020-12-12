using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class GameModeRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<GameMode>> GetGameModesAsync()
        {
            return await context.GameMode.ToListAsync();
        }

        public async Task<GameMode> GetGameModeByIdAsync(int id)
        {
            return await context.GameMode.FindAsync(id);
        }

        #endregion

        #region Update Functions
        public async Task<GameMode> UpdateGameModeAsync(GameMode gameMode)
        {
            if (gameMode == null) return null;
            var gameModeToUpdate = await context.GameMode.FindAsync(gameMode.GameModeId);
            if (gameModeToUpdate == null) return null;
            gameModeToUpdate.GameModeId = gameMode.GameModeId;
            gameModeToUpdate.Name = gameMode.Name;
            var checkUpdate = await context.SaveChangesAsync() > 0;
            if (!checkUpdate) return null;
            return gameModeToUpdate;
        }
        #endregion

        #region Insert Functions
        public async Task<GameMode> InsertGameModeAsync(GameMode gameMode)
        {
            if (gameMode == null) return null;
            context.GameMode.Add(gameMode);
            var checkInsert = await context.SaveChangesAsync() > 0;
            if (!checkInsert) return null;
            return gameMode;
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGameModeByIdAsync(int id)
        {
            var checkGameModeInGames = await context.Game_GameMode.Where(g => g.GameModeId == id).ToListAsync();
            if (checkGameModeInGames.Count() != 0) throw new ArgumentException("Dieser GameMode wird in Games verwendet und kann nicht gelöscht werden");

            var gameModeToDelete = await context.GameMode.FindAsync(id);
            if (gameModeToDelete == null) return false;
            context.GameMode.Remove(gameModeToDelete);
            return await context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}