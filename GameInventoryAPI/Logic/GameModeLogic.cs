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
        private GameModeModel MapToModel(GameMode gameMode)
        {
            if (gameMode == null) return null;

            return new GameModeModel
            {
                GameModeId = gameMode.GameModeId,
                Name = gameMode.Name
            };
        } 
    }
}