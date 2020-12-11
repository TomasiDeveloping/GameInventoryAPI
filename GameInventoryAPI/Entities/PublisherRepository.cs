using GameInventoryAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GameInventoryAPI.Entities
{
    public class PublisherRepository
    {
        #region Get Functions

        private readonly GameInventoryEntities context = new GameInventoryEntities();

        public async Task<IEnumerable<Publishers>> GetPublishersAsync()
        {
            return await context.Publishers.ToListAsync();
        }

        public async Task<Publishers> GetPublisherByIdAsync(int id)
        {
            return await context.Publishers.FindAsync(id);
        }

        #endregion

        #region Update Functions
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
        #endregion

        #region Insert Functions
        public async Task<Publishers> InsertPublisherAsync(Publishers publisher)
        {
            if (publisher == null) return null;

            context.Publishers.Add(publisher);

            var check = await context.SaveChangesAsync() > 0;

            return !check ? null : publisher;
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeletePublisherByIdAsync(int publisherId)
        {
            if (publisherId <= 0) return false;

            var publisherToDelete = await context.Publishers.FindAsync(publisherId);
            if (publisherToDelete == null) return false;

            context.Publishers.Remove(publisherToDelete);

            return await context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}