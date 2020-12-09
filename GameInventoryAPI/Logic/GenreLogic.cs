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

        public GenreModel MapToModel(Genres genre)
        {
            if (genre == null) return null;

            return new GenreModel
            {
                Description = genre.Description,
                GenreId = genre.GenreId,
                Name = genre.Name
            };
        }
    }
}