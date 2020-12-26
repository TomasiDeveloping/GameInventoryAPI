using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class GameModeLogic
    {
        private readonly GameModeRepository repository = new GameModeRepository();

        #region Get Functions

        /// <summary>
        /// Get all GameModeModel Async
        /// </summary>
        /// <returns>IEnumerable of GameModeModel</returns>
        public async Task<IEnumerable<GameModeModel>> GetGameModesAsync()
        {
            var gameModes = await repository.GetGameModesAsync();
            var modelList = new List<GameModeModel>();
            foreach (var item in gameModes) modelList.Add(MapToModel(item));
            return modelList;
        }

        /// <summary>
        /// Get a GameModeModel by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GameModeModel</returns>
        public async Task<GameModeModel> GetGameModeByIdAsync(int id)
        {
            var gameMode = await repository.GetGameModeByIdAsync(id);
            if (gameMode == null) return null;
            return MapToModel(gameMode);
        }

        /// <summary>
        /// Get a GameModeModel by Name Async
        /// </summary>
        /// <param name="gameModeName"></param>
        /// <returns>GameModeModel</returns>
        public async Task<GameModeModel> GetGameModeByNameAsyn(string gameModeName)
        {
            var gameMode = await repository.GetGameModeByNameAsync(gameModeName);
            if (gameMode == null) return null;
            return MapToModel(gameMode);
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a GameModeModel Async
        /// </summary>
        /// <param name="gameModeModel"></param>
        /// <returns>GameModeModel</returns>
        public async Task<GameModeModel> UpdateGameModeAsync(GameModeModel gameModeModel)
        {
            if (gameModeModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateGameModeAsync(MapToDbObject(gameModeModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new GameModeModel Async
        /// </summary>
        /// <param name="gameModeModel"></param>
        /// <returns>GameModeModel</returns>
        public async Task<GameModeModel> InsertGameModeAsync(GameModeModel gameModeModel)
        {
            if (gameModeModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertGameModeAsync(MapToDbObject(gameModeModel)));
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a GameMode by Id Async
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteGameModeById(int gameModeId)
        {
            return await repository.DeleteGameModeByIdAsync(gameModeId);
        }

        /// <summary>
        /// Delete a GameMode by Id with all dependencies Async
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <returns></returns>
        public async Task<bool> ForceDeleteGameModeByIdAsync(int gameModeId)
        {
            return await repository.ForceDeleteGameModeByIdAsync(gameModeId);
        }

        #endregion Delete Functions

        /// <summary>
        /// Map a DB GameMode Object to a GameModeModel
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns>GameModeModel</returns>
        private GameModeModel MapToModel(GameMode gameMode)
        {
            if (gameMode == null) return null;

            return new GameModeModel
            {
                GameModeId = gameMode.GameModeId,
                Name = gameMode.Name
            };
        }

        /// <summary>
        /// Map a GameModeModel to a DB GameMode Object
        /// </summary>
        /// <param name="gameModeModel"></param>
        /// <returns></returns>
        private GameMode MapToDbObject(GameModeModel gameModeModel)
        {
            if (gameModeModel == null) return null;

            return new GameMode
            {
                GameModeId = gameModeModel.GameModeId,
                Name = gameModeModel.Name
            };
        }
    }
}