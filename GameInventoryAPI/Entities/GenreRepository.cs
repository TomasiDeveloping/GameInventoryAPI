using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class GenreRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Genres>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<Genres> GetGenreByIdAsync(int id)
        {
            return await context.Genres.FindAsync(id);
        }

        #endregion

        #region Update Functions
        public async Task<Genres> UpdateGenreAsync(Genres genre)
        {
            if (genre == null) return null;

            var genreToUpdate = await context.Genres.FindAsync(genre.GenreId);
            if (genreToUpdate == null) return null;

            genreToUpdate.GenreId = genre.GenreId;
            genreToUpdate.Name = genre.Name;
            genreToUpdate.Description = genre.Description;

            var checke = await context.SaveChangesAsync() > 0;
            if (!checke) return null;

            return genreToUpdate;
        }
        #endregion

        #region Insert Functions
        public async Task<Genres> InsertGenreAsync(Genres genre)
        {
            if (genre == null) return null;

            context.Genres.Add(genre);

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return genre;
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteGenreByIdAsync(int genreId)
        {
            if (genreId <= 0) return false;
            var checkGameWithGenre = await context.Game_Genre.Where(g => g.GenreId == genreId).ToListAsync();
            if (checkGameWithGenre.Count() != 0) throw new ArgumentException("Dieses Genre wird in Games verwendet und kann nicht gelöscht werden");

            var genreToDelete = await context.Genres.FindAsync(genreId);
            if (genreToDelete == null) return false;

            context.Genres.Remove(genreToDelete);
            return await context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}