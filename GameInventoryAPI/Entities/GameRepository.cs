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

        public async Task<Games> GetGameByIdAsync(int gameId)
        {
            return await context.Games.FindAsync(gameId);
        }

        public async Task<IEnumerable<Games>> GetGamesByPlattformIdAsync(int plattformId)
        {
            return await context.Games.Where(g => g.Game_Plattform.Any(x => x.PlattformId == plattformId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByPublisherIdAsync(int publisherId)
        {
            return await context.Games.Where(g => g.PublisherId == publisherId).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByMediumIdAsync(int mediumId)
        {
            return await context.Games.Where(g => g.Game_Medium.Any(x => x.MediumId == mediumId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByGenreIdAsync(int genreId)
        {
            return await context.Games.Where(g => g.Game_Genre.Any(x => x.GenreId == genreId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByGameModeIdAsync(int gameModeId)
        {
            return await context.Games.Where(g => g.Game_GameMode.Any(x => x.GameModeId == gameModeId)).ToListAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesByEngineIdAsync(int engineId)
        {
            return await context.Games.Where(g => g.GameEngineId == engineId).ToListAsync();
        }
    }
}