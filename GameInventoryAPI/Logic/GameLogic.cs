using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class GameLogic
    {
        private readonly GameRepository gameRepository = new GameRepository();

        #region Get Functions

        /// <summary>
        /// Get all GameModel Async
        /// </summary>
        /// <returns>IEnumerable of GameModel</returns>
        public async Task<IEnumerable<GameModel>> GetGamesAsync()
        {
            var games = await gameRepository.GetGamesAsync();
            var models = new List<GameModel>();
            foreach (var game in games) models.Add(MapToModel(game));

            return models;
        }

        /// <summary>
        /// Get all GameDto Async
        /// </summary>
        /// <returns>IEnumerable of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesDtoAsync()
        {
            return await gameRepository.GetGamesDtoAsync();
        }

        /// <summary>
        /// Get all GameNames and GameIds Async
        /// </summary>
        /// <returns>IEnumerable of GameNameAndId</returns>
        public async Task<IEnumerable<GameNameAndId>> GetGamesWithNameAndIdAsync()
        {
            return await gameRepository.GetGamesNameAndIdAsync();
        }

        /// <summary>
        /// Get a GameModel by Id Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>GameModel</returns>
        public async Task<GameModel> GetGameByIdAsync(int gameId)
        {
            var game = await gameRepository.GetGameByIdAsync(gameId);
            if (game == null) return null;
            return MapToModel(game);
        }

        /// <summary>
        /// Get all GameDto by PlatformId Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <returns>IEnumerabel of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByPlattformIdAsync(int plattformId)
        {
            return await gameRepository.GetGamesByPlattformIdAsync(plattformId);
        }

        /// <summary>
        /// Get all GameModel by PublisherId Async
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns>IEnumerable of GameModel</returns>
        public async Task<IEnumerable<GameModel>> GetGamesByPublisherIdAsync(int publisherId)
        {
            var games = await gameRepository.GetGamesByPublisherIdAsync(publisherId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        /// <summary>
        /// Get all GameDto by MediumId Async
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns>IEnumerable of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByMediumIdAsync(int mediumId)
        {
            return await gameRepository.GetGamesByMediumIdAsync(mediumId);
        }

        /// <summary>
        /// Get all GameDto by GenreId Async
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>IEnumerable of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByGenreIdAsync(int genreId)
        {
            return await gameRepository.GetGamesByGenreIdAsync(genreId);
        }

        /// <summary>
        /// Get all GameDto by GameModeId Async
        /// </summary>
        /// <param name="gameModeId"></param>
        /// <returns>IEnumerable of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByGameModeIdAsync(int gameModeId)
        {
            return await gameRepository.GetGamesByGameModeIdAsync(gameModeId);
        }

        /// <summary>
        /// Get all GameModel by EngineId Async
        /// </summary>
        /// <param name="engineId"></param>
        /// <returns>IEnumerable of GameModel</returns>
        public async Task<IEnumerable<GameModel>> GetGamesByEngineIdAsync(int engineId)
        {
            var games = await gameRepository.GetGamesByEngineIdAsync(engineId);
            var modelList = new List<GameModel>();
            foreach (var item in games) modelList.Add(MapToModel(item));
            return modelList;
        }

        /// <summary>
        /// Get a GameModel by GameName Async
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>GameModel</returns>
        public async Task<GameModel> GetGameByNameAsync(string gameName)
        {
            var game = await gameRepository.GetGameByNameAsync(gameName);
            if (game == null) return null;
            return MapToModel(game);
        }

        /// <summary>
        /// Get all GameDto filtered by Params
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns>IEnumerable of GameDto</returns>
        public async Task<IEnumerable<GameDto>> GetGamesByFilterParamsAsync(GameFilterParams filterParams)
        {
            if (filterParams.PlattformId > 0 && filterParams.GenreId == 0 && filterParams.GameModeId == 0)
            {
                return await gameRepository.GetGamesByPlattformIdAsync(filterParams.PlattformId);
            }
            else if (filterParams.PlattformId == 0 && filterParams.GenreId > 0 && filterParams.GameModeId == 0)
            {
                return await gameRepository.GetGamesByGenreIdAsync(filterParams.GenreId);
            }
            else if (filterParams.PlattformId == 0 && filterParams.GenreId == 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByGameModeIdAsync(filterParams.GameModeId);
            }
            else if (filterParams.PlattformId > 0 && filterParams.GenreId > 0 && filterParams.GameModeId == 0)
            {
                return await gameRepository.GetGamesByPlattformAndGenreIdAsync(filterParams.PlattformId, filterParams.GenreId);
            }
            else if (filterParams.PlattformId > 0 && filterParams.GenreId == 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByPlattformAndGameModeIdAsync(filterParams.PlattformId, filterParams.GameModeId);
            }
            else if (filterParams.PlattformId == 0 && filterParams.GenreId > 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByGameModeAndGenreIdAsync(filterParams.GameModeId, filterParams.GenreId);
            }
            else if (filterParams.PlattformId > 0 && filterParams.GenreId > 0 && filterParams.GameModeId > 0)
            {
                return await gameRepository.GetGamesByPlatfformByGenreByGameModeIdAsync(filterParams.PlattformId, filterParams.GenreId, filterParams.GameModeId);
            }
            else
            {
                return null;
            }
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a GameModel Async
        /// </summary>
        /// <param name="gameModel"></param>
        /// <returns>GameModel</returns>
        public async Task<GameModel> UpdateGameAsync(GameModel gameModel)
        {
            if (gameModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await gameRepository.UpdateGameAsync(MapToDbObject(gameModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new GameModel Async
        /// </summary>
        /// <param name="gameModel"></param>
        /// <returns>GameModel</returns>
        public async Task<GameModel> InsertGameAsync(GameModel gameModel)
        {
            if (gameModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            var newGame = await gameRepository.InsertGameAsync(MapToDbObject(gameModel));

            // if a game has medium insert all medium
            if (gameModel.Mediums.Count() != 0)
            {
                foreach (var medium in gameModel.Mediums)
                {
                    await InsertMediumToGame(newGame.GameId, medium.MediumId);
                }
            }
            // if a game has Genre insert all genre
            if (gameModel.Genres.Count() != 0)
            {
                foreach (var genre in gameModel.Genres)
                {
                    await InsertGenreToGame(newGame.GameId, genre.GenreId);
                }
            }
            // if a game has platform insert all platform
            if (gameModel.Plattforms.Count() != 0)
            {
                foreach (var plattform in gameModel.Plattforms)
                {
                    await InsertPlattformToGame(newGame.GameId, plattform.PlattformId);
                }
            }
            // if a game has gameMode insert all gameMode
            if (gameModel.GameModes.Count() != 0)
            {
                foreach (var mode in gameModel.GameModes)
                {
                    await InsertGameModeToGame(newGame.GameId, mode.GameModeId);
                }
            }
            gameModel.GameId = newGame.GameId;
            return gameModel;
        }

        /// <summary>
        /// Insert Plattform to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertPlattformToGame(int gameId, int plattformId)
        {
            return await gameRepository.InsertPlattformToGameAsync(gameId, plattformId);
        }

        /// <summary>
        /// Insert a Genre to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertGenreToGame(int gameId, int genreId)
        {
            return await gameRepository.InsertGenreToGameAsync(gameId, genreId);
        }

        /// <summary>
        /// Insert a Medium to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertMediumToGame(int gameId, int mediumId)
        {
            return await gameRepository.InsertMediumToGameAsync(gameId, mediumId);
        }

        /// <summary>
        /// Insert a GameMode to a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> InsertGameModeToGame(int gameId, int gameModeId)
        {
            return await gameRepository.InsertGameModeToGameAsync(gameId, gameModeId);
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Game by Id Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteGameByIdAsync(int gameId)
        {
            return await gameRepository.DeleteGameByIdAsync(gameId);
        }

        /// <summary>
        /// Delete a Platform from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemovePlattformFromGame(int gameId, int plattformId)
        {
            return await gameRepository.RemovePlattformFromGame(gameId, plattformId);
        }

        /// <summary>
        /// Delete a Genre from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveGenreFromGame(int gameId, int genreId)
        {
            return await gameRepository.RemoveGenreFromGameAsync(gameId, genreId);
        }

        /// <summary>
        /// Delete a Medium from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveMediumFromGame(int gameId, int mediumId)
        {
            return await gameRepository.RemoveMediumFromGameAsync(gameId, mediumId);
        }

        /// <summary>
        /// Delete a GameMode from a Game Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveGameModeFromGame(int gameId, int gameModeId)
        {
            return await gameRepository.RemoveGameModeFromGameAsync(gameId, gameModeId);
        }

        #endregion Delete Functions

        #region Helper

        /// <summary>
        /// Check if a Platform already exists Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckPlattformExists(int gameId, int plattformId)
        {
            return await gameRepository.CheckPlattformExistsAsync(gameId, plattformId);
        }

        /// <summary>
        /// Check if a GameMode already exists Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="gameModeId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckGameModeExitsAsync(int gameId, int gameModeId)
        {
            return await gameRepository.CheckGameModeExistsAsync(gameId, gameModeId);
        }

        /// <summary>
        /// Check if a Genre already exists Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckGenreExitst(int gameId, int genreId)
        {
            return await gameRepository.CheckGenreExistsAsync(gameId, genreId);
        }

        /// <summary>
        /// Check if a Medium already exists Async
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckMediumExitsAsync(int gameId, int mediumId)
        {
            return await gameRepository.CheckMediumExits(gameId, mediumId);
        }

        #endregion Helper

        /// <summary>
        /// Map a DB Games Object to a GameModel Object Async
        /// </summary>
        /// <param name="game"></param>
        /// <returns>GameModel</returns>
        private GameModel MapToModel(Games game)
        {
            return new GameModel
            {
                GameId = game.GameId,
                Name = game.Name,
                GameEngineId = game.GameEngineId,
                PublisherName = game.Publishers.Name,
                PublisherId = game.PublisherId,
                AgeRating = game.AgeRating,
                CoverUrl = game.CoverUrl,
                Description = game.Description,
                FirstPublication = game.FirstPublication,
                GameEngineName = game.GameEngines?.Name,
                Information = game.Information,
                Plattforms = game.Game_Plattform?.Select(p => new GameModelPlattform { PlattformId = p.PlattformId, PlattformName = p.Plattform.Name }),
                Genres = game.Game_Genre?.Select(g => new GameModelGenre { GenreId = g.GenreId, GenreName = g.Genres.Name }),
                Mediums = game.Game_Medium?.Select(m => new GameModelMedium { MediumId = m.MediumId, MediumName = m.Medium.Name }),
                GameModes = game.Game_GameMode?.Select(m => new GameModelGameMode { GameModeId = m.GameModeId, GameModeName = m.GameMode.Name }),
            };
        }

        /// <summary>
        /// Map a GameModel to a DB Games Object
        /// </summary>
        /// <param name="gameModel"></param>
        /// <returns>Games</returns>
        private Games MapToDbObject(GameModel gameModel)
        {
            if (gameModel == null) return null;

            return new Games
            {
                GameId = gameModel.GameId,
                Name = gameModel.Name,
                PublisherId = gameModel.PublisherId,
                GameEngineId = gameModel.GameEngineId,
                AgeRating = gameModel.AgeRating,
                CoverUrl = gameModel.CoverUrl,
                Description = gameModel.Description,
                FirstPublication = gameModel.FirstPublication,
                Information = gameModel.Information,
            };
        }
    }
}