using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class GenreLogic
    {
        private readonly GenreRepository repository = new GenreRepository();

        #region Get Functions

        /// <summary>
        /// Get all GenreModel Async
        /// </summary>
        /// <returns>IEnumerable of GenreModel</returns>
        public async Task<IEnumerable<GenreModel>> GetGenresAsync()
        {
            var genres = await repository.GetGenresAsync();
            var modelList = new List<GenreModel>();
            foreach (var item in genres) modelList.Add(MapToModel(item));
            return modelList;
        }

        /// <summary>
        /// Get a GenreModel by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GenreModel</returns>
        public async Task<GenreModel> GetGenreByIdAsync(int id)
        {
            var genre = await repository.GetGenreByIdAsync(id);
            if (genre == null) return null;
            return MapToModel(genre);
        }

        /// <summary>
        /// Get a GenreModel by Name Async
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns>GenreModel</returns>
        public async Task<GenreModel> GetGenreByNameAsync(string genreName)
        {
            var genre = await repository.GetGenreByNameAsync(genreName);
            if (genre == null) return null;
            return MapToModel(genre);
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a GenreModel Async
        /// </summary>
        /// <param name="genreModel"></param>
        /// <returns>GenreModel</returns>
        public async Task<GenreModel> UpdateGenreAsync(GenreModel genreModel)
        {
            if (genreModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateGenreAsync(MapToDbObject(genreModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new GenreModel Async
        /// </summary>
        /// <param name="genreModel"></param>
        /// <returns>GenreModel</returns>
        public async Task<GenreModel> InsertGenreAsync(GenreModel genreModel)
        {
            if (genreModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertGenreAsync(MapToDbObject(genreModel)));
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Genre by Id Async
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteGenreByIdAsync(int genreId)
        {
            return await repository.DeleteGenreByIdAsync(genreId);
        }

        /// <summary>
        /// Delete a Genre by Id with all dependencies Async
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDelteGenreByIdAsync(int genreId)
        {
            return await repository.ForecDeleteGenreByIdAsync(genreId);
        }

        #endregion Delete Functions

        /// <summary>
        /// Map a DB Genres Object to a GenreModel
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>GenreModel</returns>
        private GenreModel MapToModel(Genres genre)
        {
            if (genre == null) return null;

            return new GenreModel
            {
                Description = genre.Description,
                GenreId = genre.GenreId,
                Name = genre.Name
            };
        }

        /// <summary>
        /// Map a GenreModel to a Genres DB Object
        /// </summary>
        /// <param name="genreModel"></param>
        /// <returns>Genres</returns>
        private Genres MapToDbObject(GenreModel genreModel)
        {
            if (genreModel == null) return null;

            return new Genres
            {
                Description = genreModel.Description,
                GenreId = genreModel.GenreId,
                Name = genreModel.Name
            };
        }
    }
}