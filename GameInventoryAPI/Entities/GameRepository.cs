using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class GameRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();
        public async Task<IEnumerable<Games>> GetGamesAsync()
        {
            return await context.Games.ToListAsync();
        }

        public async Task<Games> GetGameById(int gameId)
        {
            return await context.Games.FindAsync(gameId);
        }

        public async Task<IEnumerable<Games>> GetGamesByPlattformId(int plattformId)
        {
            return await context.Games.Where(g => g.Game_Plattform.Any(x => x.PlattformId == plattformId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByPublisherId(int publisherId)
        {
            return await context.Games.Where(g => g.PublisherId == publisherId).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByMediumId(int mediumId)
        {
            return await context.Games.Where(g => g.Game_Medium.Any(x => x.MediumId == mediumId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByGenreId(int genreId)
        {
            return await context.Games.Where(g => g.Game_Genre.Any(x => x.GenreId == genreId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByGameModeId(int gameModeId)
        {
            return await context.Games.Where(g => g.Game_GameMode.Any(x => x.GameModeId == gameModeId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByEngineId(int engineId)
        {
            return await context.Games.Where(g => g.GameEngineId == engineId).ToListAsync();
        }
    }
}