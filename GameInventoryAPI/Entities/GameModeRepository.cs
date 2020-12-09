using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class GameModeRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<GameMode>> GetGameModesAsync()
        {
            return await context.GameMode.ToListAsync();
        }

        public async Task<GameMode> GetGameModeByIdAsync(int id)
        {
            return await context.GameMode.FindAsync(id);
        }
    }
}