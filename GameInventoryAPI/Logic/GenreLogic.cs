using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Logic
{
    public class GenreLogic
    {
        private readonly GenreRepository repository = new GenreRepository();

        #region Get Functions
        public async Task<IEnumerable<GenreModel>> GetGenresAsync()
        {
            var genres = await repository.GetGenresAsync();
            var modelList = new List<GenreModel>();
            foreach (var item in genres) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<GenreModel> GetGenreByIdAsync(int id)
        {
            var genre = await repository.GetGenreByIdAsync(id);
            if (genre == null) return null;
            return MapToModel(genre);
        }

        public async Task<GenreModel> GetGenreByNameAsync(string genreName)
        {
            var genre = await repository.GetGenreByNameAsync(genreName);
            if (genre == null) return null;
            return MapToModel(genre);
        }
        #endregion

        #region Update Functions
        public async Task<GenreModel> UpdateGenreAsync(GenreModel genreModel)
        {
            if (genreModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateGenreAsync(MapToDbObject(genreModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<GenreModel> InsertGenreAsync(GenreModel genreModel)
        {
            if (genreModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertGenreAsync(MapToDbObject(genreModel)));
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGenreByIdAsync(int genreId)
        {
            return await repository.DeleteGenreByIdAsync(genreId);
        }

        public async Task<bool> ForceDelteGenreByIdAsync(int genreId)
        {
            return await repository.ForecDeleteGenreByIdAsync(genreId);
        }
        #endregion

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