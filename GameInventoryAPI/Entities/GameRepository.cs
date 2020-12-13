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

        public async Task<Games> GetGameByNameAsync(string gameName)
        {
            return await context.Games.FirstOrDefaultAsync(g => g.Name.Equals(gameName));
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

        public async Task<bool> InsertPlattformToGameAsync(int gameId, int plattformId)
        {
            if (gameId <= 0 || plattformId <= 0) return false;
            var game_Plattform = new Game_Plattform { GameId = gameId, PlattformId = plattformId };
            context.Game_Plattform.Add(game_Plattform);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertMediumToGameAsync(int gameId, int mediumId)
        {
            if (gameId <= 0 || mediumId <= 0) return false;
            var game_Medium = new Game_Medium { GameId = gameId, MediumId = mediumId };
            context.Game_Medium.Add(game_Medium);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertGenreToGameAsync(int gameId, int genreId)
        {
            if (gameId <= 0 || genreId <= 0) return false;
            var game_Genre = new Game_Genre { GameId = gameId, GenreId = gameId };
            context.Game_Genre.Add(game_Genre);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertGameModeToGameAsync(int gameId, int gameModeId)
        {
            if (gameId <= 0 || gameModeId <= 0) return false;
            var game_GameMode = new Game_GameMode { GameId = gameId, GameModeId = gameModeId };
            context.Game_GameMode.Add(game_GameMode);
            return await context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGameByIdAsync(int id)
        {
            var gameToDelete = await context.Games.FindAsync(id);
            if (gameToDelete == null) return false;

            // Check if the game has GameModes and remove it
            var gameModeList = await context.Game_GameMode.Where(g => g.GameId == id).ToListAsync();
            if (gameModeList.Count() > 0)
            {
                foreach (var item in gameModeList)
                {
                    var gameMode = await context.Game_GameMode.FindAsync(item.GameGameModeId);
                    context.Game_GameMode.Remove(gameMode);
                }
            }

            // Check if the game has plattforms and remove it
            var plattformList = await context.Game_Plattform.Where(g => g.GameId == id).ToListAsync();
            if (plattformList.Count() > 0)
            {
                foreach (var item in plattformList)
                {
                    var plattform = await context.Game_Plattform.FindAsync(item.GamePlattformId);
                    context.Game_Plattform.Remove(plattform);
                }
            }

            // Check if the game has mediums and remove it
            var mediumList = await context.Game_Medium.Where(g => g.GameId == id).ToListAsync();
            if (mediumList.Count() > 0)
            {
                foreach (var item in mediumList)
                {
                    var medium = await context.Game_Medium.FindAsync(item.GameMediumId);
                    context.Game_Medium.Remove(medium);
                }
            }

            // Check if the game has genres and remove it
            var genreList = await context.Game_Genre.Where(g => g.GameGenreId == id).ToListAsync();
            if (genreList.Count() > 0)
            {
                foreach (var item in genreList)
                {
                    var genre = await context.Game_Genre.FindAsync(item.GameGenreId);
                    context.Game_Genre.Remove(genre);
                }
            }

            context.Games.Remove(gameToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveGameModeFromGameAsync(int gameId, int gameModeId)
        {
            if (gameId <= 0 || gameId <= 0) return false;
            var gameGameModeToRemove = await context.Game_GameMode.FirstOrDefaultAsync(g => g.GameId == gameId && g.GameModeId == gameModeId);
            if (gameGameModeToRemove == null) throw new ArgumentException("Kein GameMode gefunden");

            context.Game_GameMode.Remove(gameGameModeToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemovePlattformFromGame(int gameId, int plattformId)
        {
            if (gameId <= 0 || plattformId <= 0) return false;
            var gamePlattformToRemove = await context.Game_Plattform.FirstOrDefaultAsync(g => g.GameId == gameId && g.PlattformId == plattformId);

            if (gamePlattformToRemove == null) throw new ArgumentException("Keine Plattform gefunden");
            context.Game_Plattform.Remove(gamePlattformToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveGenreFromGameAsync(int gameId, int genreId)
        {
            if (gameId <= 0 || genreId <= 0) return false;
            var gameGenreToRemove = await context.Game_Genre.FirstOrDefaultAsync(g => g.GameId == gameId && g.GameId == genreId);

            if (gameGenreToRemove == null) throw new ArgumentException("Kein Genre gefunden");
            context.Game_Genre.Remove(gameGenreToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveMediumFromGameAsync(int gameId, int mediumId)
        {
            if (gameId <= 0 || mediumId <= 0) return false;
            var gameMediumToRemove = await context.Game_Medium.FirstOrDefaultAsync(g => g.GameId == gameId && g.MediumId == mediumId);

            if (gameMediumToRemove == null) throw new ArgumentException("Kein Medium gefunden");
            context.Game_Medium.Remove(gameMediumToRemove);
            return await context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}