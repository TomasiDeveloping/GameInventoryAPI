using GameInventoryAPI.Data;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class GameRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        #region Get Functions

        /// <summary>
        /// Get all Games Async
        /// </summary>
        /// <returns>IEnumerable of Games</returns>
        public async Task<IEnumerable<Games>> GetGamesAsync()
        {
            return await context.Games.ToListAsync();
        }

        /// <summary>
        /// Get all GameDtos Async
        /// </summary>
        /// <returns>IEnumerable of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesDtoAsync()
        {
            return await context.Games
                .Select(g => new GameDto
                {
                    GameId = g.GameId,
                    GameName = g.Name,
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Game Names and Ids Async
        /// </summary>
        /// <returns>IEnumerable of GameNameAndId</returns>
        public async Task<IEnumerable<GameNameAndId>> GetGamesNameAndIdAsync()
        {
            return await context.Games.Select(x => new GameNameAndId
            {
                GameName = x.Name,
                GameId = x.GameId
            }).ToListAsync();
        }

        /// <summary>
        /// Get Game by Id Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Games</returns>
        public async Task<Games> GetGameByIdAsync(int gameId)
        {
            return await context.Games.FindAsync(gameId);
        }

        /// <summary>
        /// Get Games by PlattformId Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByPlattformIdAsync(int plattformId)
        {
            return await context.Games
                .Where(g => g.Game_Plattform.Any(x => x.PlattformId == plattformId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by PublisherId Async
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns>IEnumerable of Games</returns>
        public async Task<IEnumerable<Games>> GetGamesByPublisherIdAsync(int publisherId)
        {
            return await context.Games.Where(g => g.PublisherId == publisherId).ToListAsync();
        }

        /// <summary>
        /// Get Games by MediumId Async
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByMediumIdAsync(int mediumId)
        {
            return await context.Games
                .Where(g => g.Game_Medium.Any(m => m.MediumId == mediumId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by GenreId Async
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByGenreIdAsync(int genreId)
        {
            return await context.Games
                .Where(g => g.Game_Genre.Any(x => x.GenreId == genreId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by GameModeId Async
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByGameModeIdAsync(int gameModeId)
        {
            return await context.Games
                .Where(g => g.Game_GameMode.Any(x => x.GameModeId == gameModeId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by PlattformId and GenreId Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <param name="genreId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByPlattformAndGenreIdAsync(int plattformId, int genreId)
        {
            return await context.Games
                .Where(g => g.Game_Plattform.Any(x => x.PlattformId == plattformId))
                .Where(g => g.Game_Genre.Any(x => x.GenreId == genreId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by PlattformId and GameModeId Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByPlattformAndGameModeIdAsync(int plattformId, int gameModeId)
        {
            return await context.Games
                .Where(g => g.Game_Plattform.Any(x => x.PlattformId == plattformId))
                .Where(g => g.Game_GameMode.Any(x => x.GameModeId == gameModeId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by GameModeId and GenreId Async
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <param name="genreId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByGameModeAndGenreIdAsync(int gameModeId, int genreId)
        {
            return await context.Games
                .Where(g => g.Game_GameMode.Any(x => x.GameModeId == gameModeId))
                .Where(g => g.Game_Genre.Any(x => x.GenreId == genreId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by PlattformId, GenreId and GameModeId Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <param name="genreId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>IEnumerable of GameDto order by GameName</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByPlatfformByGenreByGameModeIdAsync(int plattformId, int genreId, int gameModeId)
        {
            return await context.Games
                .Where(g => g.Game_Plattform.Any(x => x.PlattformId == plattformId))
                .Where(g => g.Game_Genre.Any(x => x.GenreId == genreId))
                .Where(g => g.Game_GameMode.Any(x => x.GameModeId == gameModeId))
                .Select(g => new GameDto
                {
                    CoverUrl = g.CoverUrl,
                    FirstPublication = g.FirstPublication,
                    GameId = g.GameId,
                    GameName = g.Name,
                    PublisherName = g.Publishers.Name
                })
                .OrderBy(g => g.GameName)
                .ToListAsync();
        }

        /// <summary>
        /// Get Games by EngineId Async
        /// </summary>
        /// <param name="engineId"></param>
        /// <returns>IEnumerable of Games</returns>
        public async Task<IEnumerable<Games>> GetGamesByEngineIdAsync(int engineId)
        {
            return await context.Games.Where(g => g.GameEngineId == engineId).ToListAsync();
        }

        /// <summary>
        /// Get Game by Name Async
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>Games</returns>
        public async Task<Games> GetGameByNameAsync(string gameName)
        {
            return await context.Games.FirstOrDefaultAsync(g => g.Name.Equals(gameName));
        }

        #endregion Get Functions

        /// <summary>
        /// Update a Game Async
        /// </summary>
        /// <param name="game"></param>
        /// <returns>Games</returns>

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

            await context.SaveChangesAsync();

            return gameToUpdate;
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new Game to the DB Async
        /// </summary>
        /// <param name="game"></param>
        /// <returns>Games</returns>
        public async Task<Games> InsertGameAsync(Games game)
        {
            if (game == null) return null;

            context.Games.Add(game);

            var checkInsert = await context.SaveChangesAsync() > 0;

            if (!checkInsert) return null;

            return game;
        }

        /// <summary>
        /// Insert a new Plattform to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertPlattformToGameAsync(int gameId, int plattformId)
        {
            if (gameId <= 0 || plattformId <= 0) return false;
            var game_Plattform = new Game_Plattform { GameId = gameId, PlattformId = plattformId };
            context.Game_Plattform.Add(game_Plattform);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Insert a new Medium to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertMediumToGameAsync(int gameId, int mediumId)
        {
            if (gameId <= 0 || mediumId <= 0) return false;
            var game_Medium = new Game_Medium { GameId = gameId, MediumId = mediumId };
            context.Game_Medium.Add(game_Medium);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Insert a new Genre to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertGenreToGameAsync(int gameId, int genreId)
        {
            if (gameId <= 0 || genreId <= 0) return false;
            var game_Genre = new Game_Genre { GameId = gameId, GenreId = genreId };
            context.Game_Genre.Add(game_Genre);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Insert a new GameMode to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertGameModeToGameAsync(int gameId, int gameModeId)
        {
            if (gameId <= 0 || gameModeId <= 0) return false;
            var game_GameMode = new Game_GameMode { GameId = gameId, GameModeId = gameModeId };
            context.Game_GameMode.Add(game_GameMode);
            return await context.SaveChangesAsync() > 0;
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Game from the DB by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
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
            var genreList = await context.Game_Genre.Where(g => g.GameId == id).ToListAsync();
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

        /// <summary>
        /// Delete a GameMode from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveGameModeFromGameAsync(int gameId, int gameModeId)
        {
            if (gameId <= 0 || gameId <= 0) return false;
            var gameGameModeToRemove = await context.Game_GameMode.FirstOrDefaultAsync(g => g.GameId == gameId && g.GameModeId == gameModeId);
            if (gameGameModeToRemove == null) throw new ArgumentException("Kein GameMode gefunden");

            context.Game_GameMode.Remove(gameGameModeToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a Platform from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemovePlattformFromGame(int gameId, int plattformId)
        {
            if (gameId <= 0 || plattformId <= 0) return false;
            var gamePlattformToRemove = await context.Game_Plattform.FirstOrDefaultAsync(g => g.GameId == gameId && g.PlattformId == plattformId);

            if (gamePlattformToRemove == null) throw new ArgumentException("Keine Plattform gefunden");
            context.Game_Plattform.Remove(gamePlattformToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a Genre from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveGenreFromGameAsync(int gameId, int genreId)
        {
            if (gameId <= 0 || genreId <= 0) return false;
            var gameGenreToRemove = await context.Game_Genre.FirstOrDefaultAsync(g => g.GameId == gameId && g.GenreId == genreId);

            if (gameGenreToRemove == null) throw new ArgumentException("Kein Genre gefunden");
            context.Game_Genre.Remove(gameGenreToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete a Medium from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveMediumFromGameAsync(int gameId, int mediumId)
        {
            if (gameId <= 0 || mediumId <= 0) return false;
            var gameMediumToRemove = await context.Game_Medium.FirstOrDefaultAsync(g => g.GameId == gameId && g.MediumId == mediumId);

            if (gameMediumToRemove == null) throw new ArgumentException("Kein Medium gefunden");
            context.Game_Medium.Remove(gameMediumToRemove);
            return await context.SaveChangesAsync() > 0;
        }

        #endregion Delete Functions

        #region Helper

        /// <summary>
        /// Checks if the platfrom is already present on a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckPlattformExistsAsync(int gameId, int plattformId)
        {
            var plattformCheck = await context.Game_Plattform.FirstOrDefaultAsync(g => g.GameId == gameId && g.PlattformId == plattformId);
            return plattformCheck == null ? false : true;
        }

        /// <summary>
        /// Checks if the GameMode is already present on a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckGameModeExistsAsync(int gameId, int gameModeId)
        {
            var gameModeToCheck = await context.Game_GameMode.FirstOrDefaultAsync(g => g.GameId == gameId && g.GameModeId == gameModeId);
            return gameModeToCheck == null ? false : true;
        }

        /// <summary>
        /// Checks if the Genre is already present on a Game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckGenreExistsAsync(int gameId, int genreId)
        {
            var genreToCheck = await context.Game_Genre.FirstOrDefaultAsync(g => g.GameId == gameId && g.GenreId == genreId);
            return genreToCheck == null ? false : true;
        }

        /// <summary>
        /// Checks if the Medium is already present on a Game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckMediumExits(int gameId, int mediumId)
        {
            var mediumToCheck = await context.Game_Medium.FirstOrDefaultAsync(g => g.GameId == gameId && g.MediumId == mediumId);
            return mediumToCheck == null ? false : true;
        }

        #endregion Helper
    }
}