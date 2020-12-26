using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class EngineRepository
    {
        #region GET

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        /// <summary>
        /// Get Engines Async
        /// </summary>
        /// <returns>IEnumerable of GameEngines</returns>
        public async Task<IEnumerable<GameEngines>> GetEnginesAsync()
        {
            return await context.GameEngines.ToListAsync();
        }

        /// <summary>
        /// Get Engine by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GameEngine</returns>
        public async Task<GameEngines> GetEngineByIdAsync(int id)
        {
            return await context.GameEngines.FindAsync(id);
        }

        /// <summary>
        /// Get Engine by Name Async
        /// </summary>
        /// <param name="engineName"></param>
        /// <returns>GameEngine</returns>
        public async Task<GameEngines> GetEngineByNameAsync(string engineName)
        {
            return await context.GameEngines.FirstOrDefaultAsync(e => e.Name.Equals(engineName));
        }

        #endregion GET

        #region INSERT

        /// <summary>
        /// Insert a new Engine to the DB Async
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>GameEngine</returns>
        public async Task<GameEngines> InsertEngineAsync(GameEngines gameEngine)
        {
            context.GameEngines.Add(gameEngine);
            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;
            return gameEngine;
        }

        #endregion INSERT

        #region UPDATE

        /// <summary>
        /// Update a Game Engine Async
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>GameEngine</returns>
        public async Task<GameEngines> UpdateGameEngineAsync(GameEngines gameEngine)
        {
            var engineToUpdate = await context.GameEngines.FindAsync(gameEngine.GameEngineId);
            engineToUpdate.GameEngineId = gameEngine.GameEngineId;
            engineToUpdate.Name = gameEngine.Name;

            await context.SaveChangesAsync();
            return engineToUpdate;
        }

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Delete a Game Engine by Id Async
        /// </summary>
        /// <param name="gameEngineId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteEngineByIdAsync(int gameEngineId)
        {
            if (gameEngineId <= 0) return false;

            var checkGamesWhitEngine = await context.Games.Where(e => e.GameEngineId == gameEngineId).ToListAsync();
            if (checkGamesWhitEngine.Count() != 0) throw new ArgumentException("Engine wird bei Games verwendet");

            var engineToDelete = await context.GameEngines.FindAsync(gameEngineId);
            if (engineToDelete == null) return false;
            context.GameEngines.Remove(engineToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a Game Engine and remove all dependencies Async
        /// </summary>
        /// <param name="gameEngineId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeleteEngineByIdAsync(int gameEngineId)
        {
            // Check if the engine is use in games
            var checkGames = await context.Games.Where(g => g.GameEngineId == gameEngineId).ToListAsync();
            if (checkGames.Count() > 0)
            {
                // update all games to remove the engine
                foreach (var game in checkGames)
                {
                    game.GameEngineId = null;
                }
            }

            var engineToDelete = await context.GameEngines.FindAsync(gameEngineId);

            if (engineToDelete == null) return false;
            context.GameEngines.Remove(engineToDelete);
            return await context.SaveChangesAsync() > 0;
        }

        #endregion DELETE
    }
}