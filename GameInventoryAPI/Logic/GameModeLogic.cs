using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Logic
{
    public class GameModeLogic
    {
        private readonly GameModeRepository repository = new GameModeRepository();

        #region Get Functions
        public async Task<IEnumerable<GameModeModel>> GetGameModesAsync()
        {
            var gameModes = await repository.GetGameModesAsync();
            var modelList = new List<GameModeModel>();
            foreach (var item in gameModes) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<GameModeModel> GetGameModeByIdAsync(int id)
        {
            var gameMode = await repository.GetGameModeByIdAsync(id);
            if (gameMode == null) return null;
            return MapToModel(gameMode);
        }

        public async Task<GameModeModel> GetGameModeByNameAsyn(string gameModeName)
        {
            var gameMode = await repository.GetGameModeByNameAsync(gameModeName);
            if (gameMode == null) return null;
            return MapToModel(gameMode);
        }
        #endregion

        #region Update Functions
        public async Task<GameModeModel> UpdateGameModeAsync(GameModeModel gameModeModel)
        {
            if (gameModeModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateGameModeAsync(MapToDbObject(gameModeModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<GameModeModel> InsertGameModeAsync(GameModeModel gameModeModel)
        {
            if (gameModeModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertGameModeAsync(MapToDbObject(gameModeModel)));
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGameModeById(int gameModeId)
        {
            return await repository.DeleteGameModeByIdAsync(gameModeId);
        }

        public async Task<bool> ForceDeleteGameModeByIdAsync(int gameModeId)
        {
            return await repository.ForceDeleteGameModeByIdAsync(gameModeId);
        }
        #endregion

        private GameModeModel MapToModel(GameMode gameMode)
        {
            if (gameMode == null) return null;

            return new GameModeModel
            {
                GameModeId = gameMode.GameModeId,
                Name = gameMode.Name
            };
        }
        
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