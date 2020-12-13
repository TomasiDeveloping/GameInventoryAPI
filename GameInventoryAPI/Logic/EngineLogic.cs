﻿using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Logic
{
    public class EngineLogic
    {
        private readonly EngineRepository repository = new EngineRepository();

        #region Get Functions
        public async Task<IEnumerable<GameEngineModel>> GetEnginesAsync()
        {
            var engines = await repository.GetEnginesAsync();
            var modeList = new List<GameEngineModel>();
            foreach (var item in engines) modeList.Add(MapToModel(item));
            return modeList;
        }

        public async Task<GameEngineModel> GetEngineByIdAsync(int id)
        {
            var engine = await repository.GetEngineByIdAsync(id);
            if (engine == null) return null;
            return MapToModel(engine);
        }

        public async Task<GameEngineModel> GetEngineByNameAsync(string engineName)
        {
            var engine = await repository.GetEngineByNameAsync(engineName);
            if (engine == null) return null;
            return MapToModel(engine);
        }
        #endregion

        #region Update Functions
        public async Task<GameEngineModel> UpdateEngineAsync(GameEngineModel gameEngineModel)
        {
            if (gameEngineModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateGameEngineAsync(MapToDbObject(gameEngineModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<GameEngineModel> InsertEngineAsync(GameEngineModel gameEngineModel)
        {
            if (gameEngineModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertEngineAsync(MapToDbObject(gameEngineModel)));
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteEngineByIdAsync(int engineId)
        {
            return await repository.DeleteEngineByIdAsync(engineId);
        }

        public async Task<bool> ForceDeleteEngineByIdAsync(int engineId)
        {
            return await repository.ForceDeleteEngineByIdAsync(engineId);
        }
        #endregion

        private GameEngineModel MapToModel(GameEngines gameEngine)
        {
            if (gameEngine == null) return null;

            return new GameEngineModel
            {
                GameEngineId = gameEngine.GameEngineId,
                Name = gameEngine.Name
            };
        }
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