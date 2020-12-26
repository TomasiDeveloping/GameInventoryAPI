using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class GenreRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        #region Get Functions

        /// <summary>
        /// Get all Genres Async
        /// </summary>
        /// <returns>IEnumerable of Genres</returns>
        public async Task<IEnumerable<Genres>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        /// <summary>
        /// Get Genre by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Genres</returns>
        public async Task<Genres> GetGenreByIdAsync(int id)
        {
            return await context.Genres.FindAsync(id);
        }

        /// <summary>
        /// Get a Genre by Name Async
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns>Genres</returns>
        public async Task<Genres> GetGenreByNameAsync(string genreName)
        {
            return await context.Genres.FirstOrDefaultAsync(g => g.Name.Equals(genreName));
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a Genre Async
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>Genres</returns>
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

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new Genre to the DB Async
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>Genres</returns>
        public async Task<Genres> InsertGenreAsync(Genres genre)
        {
            if (genre == null) return null;

            context.Genres.Add(genre);

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;

            return genre;
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Genre from the DB by Id Async
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Delete a Genre from the DB by Id with all dependencies Async
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForecDeleteGenreByIdAsync(int genreId)
        {
            var checkGenreToDelete = await context.Genres.FindAsync(genreId);
            if (checkGenreToDelete == null) return false;

            var gamesWhitGenres = await context.Game_Genre.Where(g => g.GenreId == genreId).ToListAsync();

            if (gamesWhitGenres.Count() != 0)
            {
                foreach (var item in gamesWhitGenres)
                {
                    var genre = await context.Game_Genre.FindAsync(item.GameGenreId);
                    context.Game_Genre.Remove(genre);
                }
            }

            context.Genres.Remove(checkGenreToDelete);
            return await context.SaveChangesAsync() > 0;
        }

        #endregion Delete Functions
    }
}