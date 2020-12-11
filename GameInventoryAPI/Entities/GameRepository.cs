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
        #region Get Functions

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

        #endregion

        #region Update Functions
        public async Task<Games> UpdateGameAsync(Games game)
        {
            if (game == null) return null;

            var gameToUpdate = await context.Games.FindAsync(game.GameId);

            if (gameToUpdate == null) return null;

            gameToUpdate.GameId = game.GameId;
            gameToUpdate.Name = game.Name;
            gameToUpdate.PublisherId = game.PublisherId;
            gameToUpdate.GameEngineId = game.GameEngineId;
            gameToUpdate.FirstPublication = game.FirstPublication;
            gameToUpdate.Description = game.Description;
            gameToUpdate.AgeRating = game.AgeRating;
            gameToUpdate.Information = game.Information;
            gameToUpdate.CoverUrl = game.CoverUrl;

            var checkUpdate = await context.SaveChangesAsync() > 0;

            if (!checkUpdate) return null;

            return gameToUpdate;
        }
        #endregion

        #region Insert Functions
        public async Task<Games> InsertGameAsync(Games game)
        {
            if (game == null) return null;

            context.Games.Add(game);

            var checkInsert = await context.SaveChangesAsync() > 0;

            if (!checkInsert) return null;

            return game;
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGameByIdAsync(int id)
        {
            var gameToDelete = await context.Games.FindAsync(id);

            if (gameToDelete == null) return false;

            context.Games.Remove(gameToDelete);

            return await context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}