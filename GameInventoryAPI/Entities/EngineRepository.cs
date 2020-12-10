using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class EngineRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<GameEngines>> GetEnginesAsync()
        {
            return await context.GameEngines.ToListAsync();
        }

        public async Task<GameEngines> GetEngineByIdAsync(int id)
        {
            return await context.GameEngines.FindAsync(id);
        }

        public async Task<bool> InsertEngineAsync(GameEngines gameEngine)
        {
            context.GameEngines.Add(gameEngine);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<GameEngines> UpdateGameEngineAsync(GameEngines gameEngine)
        {
            var engineToUpdate = await context.GameEngines.FindAsync(gameEngine.GameEngineId);
            engineToUpdate.GameEngineId = gameEngine.GameEngineId;
            engineToUpdate.Name = gameEngine.Name;

            await context.SaveChangesAsync();
            return engineToUpdate;
        }

        public async Task<bool> DeleteEngineById(int gameEngineId)
        {
            var engineToDelete = await context.GameEngines.FindAsync(gameEngineId);

            if (engineToDelete == null) return false;
            context.GameEngines.Remove(engineToDelete);
            return await context.SaveChangesAsync() > 0;
        }
    }
}