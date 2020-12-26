using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class EngineLogic
    {
        private readonly EngineRepository repository = new EngineRepository();

        #region Get Functions

        /// <summary>
        /// Get all GameEngineModel Async
        /// </summary>
        /// <returns>IEnumerable of GameEngineModel</returns>
        public async Task<IEnumerable<GameEngineModel>> GetEnginesAsync()
        {
            var engines = await repository.GetEnginesAsync();
            var modeList = new List<GameEngineModel>();
            foreach (var item in engines) modeList.Add(MapToModel(item));
            return modeList;
        }

        /// <summary>
        /// Get a GameEngineModel by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GameEngineModel</returns>
        public async Task<GameEngineModel> GetEngineByIdAsync(int id)
        {
            var engine = await repository.GetEngineByIdAsync(id);
            if (engine == null) return null;
            return MapToModel(engine);
        }

        /// <summary>
        /// Get a GameEngineModel by Name Async
        /// </summary>
        /// <param name="engineName"></param>
        /// <returns>GameEngineModel</returns>
        public async Task<GameEngineModel> GetEngineByNameAsync(string engineName)
        {
            var engine = await repository.GetEngineByNameAsync(engineName);
            if (engine == null) return null;
            return MapToModel(engine);
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a GameEngineModel Async
        /// </summary>
        /// <param name="gameEngineModel"></param>
        /// <returns>GameEngineModel</returns>
        public async Task<GameEngineModel> UpdateEngineAsync(GameEngineModel gameEngineModel)
        {
            if (gameEngineModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateGameEngineAsync(MapToDbObject(gameEngineModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new GameEngineModel Async
        /// </summary>
        /// <param name="gameEngineModel"></param>
        /// <returns>GameEngineModel</returns>
        public async Task<GameEngineModel> InsertEngineAsync(GameEngineModel gameEngineModel)
        {
            if (gameEngineModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertEngineAsync(MapToDbObject(gameEngineModel)));
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Engine by Id Async
        /// </summary>
        /// <param name="engineId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteEngineByIdAsync(int engineId)
        {
            return await repository.DeleteEngineByIdAsync(engineId);
        }

        /// <summary>
        /// Delete a Engine by Id with all dependencies Async
        /// </summary>
        /// <param name="engineId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeleteEngineByIdAsync(int engineId)
        {
            return await repository.ForceDeleteEngineByIdAsync(engineId);
        }

        #endregion Delete Functions

        /// <summary>
        /// Map a DB Engine to a GameEngineModel
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <returns>GameEngineModel</returns>
        private GameEngineModel MapToModel(GameEngines gameEngine)
        {
            if (gameEngine == null) return null;

            return new GameEngineModel
            {
                GameEngineId = gameEngine.GameEngineId,
                Name = gameEngine.Name
            };
        }

        /// <summary>
        /// Map a GameEngineModel to a DB GameEngine
        /// </summary>
        /// <param name="gameEngineModel"></param>
        /// <returns>GameEngine</returns>
        private GameEngines MapToDbObject(GameEngineModel gameEngineModel)
        {
            if (gameEngineModel == null) return null;

            return new GameEngines
            {
                GameEngineId = gameEngineModel.GameEngineId,
                Name = gameEngineModel.Name
            };
        }
    }
}