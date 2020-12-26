using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class GameModeRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        /// <summary>
        /// Get all GameModes Async
        /// </summary>
        /// <returns>IEnumerable of GameMode</returns>
        public async Task<IEnumerable<GameMode>> GetGameModesAsync()
        {
            return await context.GameMode.ToListAsync();
        }

        /// <summary>
        /// Get GameMode by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GameMode</returns>
        public async Task<GameMode> GetGameModeByIdAsync(int id)
        {
            return await context.GameMode.FindAsync(id);
        }

        public async Task<GameMode> GetGameModeByNameAsync(string gameModeName)
        {
            return await context.GameMode.FirstOrDefaultAsync(m => m.Name.Equals(gameModeName));
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a GameMode Async
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns>GameMode</returns>
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

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new GameMode to the DB Async
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns>GameMode</returns>
        public async Task<GameMode> InsertGameModeAsync(GameMode gameMode)
        {
            if (gameMode == null) return null;
            context.GameMode.Add(gameMode);
            var checkInsert = await context.SaveChangesAsync() > 0;
            if (!checkInsert) return null;
            return gameMode;
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a GameMode by Id from the DB Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteGameModeByIdAsync(int id)
        {
            var checkGameModeInGames = await context.Game_GameMode.Where(g => g.GameModeId == id).ToListAsync();
            if (checkGameModeInGames.Count() != 0) throw new ArgumentException("Dieser GameMode wird in Games verwendet und kann nicht gelöscht werden");

            var gameModeToDelete = await context.GameMode.FindAsync(id);
            if (gameModeToDelete == null) return false;
            context.GameMode.Remove(gameModeToDelete);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a GameMode by Id with all dependencies Async
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeleteGameModeByIdAsync(int gameModeId)
        {
            var checkGameMode = await context.GameMode.FindAsync(gameModeId);
            if (checkGameMode == null) return false;

            var gamesWhitGameModes = await context.Game_GameMode.Where(m => m.GameModeId == gameModeId).ToListAsync();

            if (gamesWhitGameModes.Count() != 0)
            {
                foreach (var mode in gamesWhitGameModes)
                {
                    var gameMode = await context.Game_GameMode.FindAsync(mode.GameGameModeId);
                    context.Game_GameMode.Remove(gameMode);
                }
            }
            context.GameMode.Remove(checkGameMode);

            return await context.SaveChangesAsync() > 0;
        }

        #endregion Delete Functions
    }
}