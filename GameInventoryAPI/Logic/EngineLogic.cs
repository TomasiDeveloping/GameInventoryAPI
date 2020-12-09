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
    public class EngineLogic
    {
        private readonly EngineRepository repository = new EngineRepository();

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

        private GameEngineModel MapToModel(GameEngines gameEngine)
        {
            if (gameEngine == null) return null;

            return new GameEngineModel
            {
                GameEngineId = gameEngine.GameEngineId,
                Name = gameEngine.Name
            };
        }
    }
}