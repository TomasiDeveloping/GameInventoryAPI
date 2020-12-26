using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameInventoryAPI.Entities
{
    public class PublisherRepository
    {
        private readonly GameInventoryEntities context = new GameInventoryEntities();

        #region Get Functions

        /// <summary>
        /// Get all Publishers Async
        /// </summary>
        /// <returns>IEnumerable of Publishers</returns>
        public async Task<IEnumerable<Publishers>> GetPublishersAsync()
        {
            return await context.Publishers.ToListAsync();
        }

        /// <summary>
        /// Get a Publisher by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Publishers</returns>
        public async Task<Publishers> GetPublisherByIdAsync(int id)
        {
            return await context.Publishers.FindAsync(id);
        }

        /// <summary>
        /// Get a Publisher by Name Async
        /// </summary>
        /// <param name="publisherName"></param>
        /// <returns>Publishers</returns>
        public async Task<Publishers> GetPublisherByNameAsync(string publisherName)
        {
            return await context.Publishers.FirstOrDefaultAsync(p => p.Name.Equals(publisherName));
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a Publisher Async
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns>Publishers</returns>
        public async Task<Publishers> UpdatePublisherAsync(Publishers publisher)
        {
            if (publisher == null) return null;

            var publisherToUpdate = await context.Publishers.FindAsync(publisher.PublisherId);
            if (publisherToUpdate == null) return null;

            publisherToUpdate.PublisherId = publisher.PublisherId;
            publisherToUpdate.Name = publisher.Name;
            publisherToUpdate.Country = publisher.Country;
            publisherToUpdate.Description = publisher.Description;

            var check = await context.SaveChangesAsync() > 0;
            if (!check) return null;
            return publisherToUpdate;
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new Publisher to the DB Async
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns>Publishers</returns>
        public async Task<Publishers> InsertPublisherAsync(Publishers publisher)
        {
            if (publisher == null) return null;

            context.Publishers.Add(publisher);

            var check = await context.SaveChangesAsync() > 0;

            return !check ? null : publisher;
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Publisher from the DB by Id Async
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeletePublisherByIdAsync(int publisherId)
        {
            if (publisherId <= 0) return false;
            var checkPublisherInGames = await context.Games.Where(g => g.PublisherId == publisherId).ToListAsync();
            if (checkPublisherInGames.Count() != 0) throw new ArgumentException("Dieser Publisher wird in Games verwendet und kann nicht gelöscht werden");

            var publisherToDelete = await context.Publishers.FindAsync(publisherId);
            if (publisherToDelete == null) return false;

            context.Publishers.Remove(publisherToDelete);

            return await context.SaveChangesAsync() > 0;
        }

        #endregion Delete Functions
    }
}