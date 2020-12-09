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
    }
}