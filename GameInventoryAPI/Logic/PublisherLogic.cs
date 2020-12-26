using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class PublisherLogic
    {
        private readonly PublisherRepository repository = new PublisherRepository();

        #region Get Functions

        /// <summary>
        /// Get all PublisherModel Async
        /// </summary>
        /// <returns>IEnumerable of PublisherModel</returns>
        public async Task<IEnumerable<PublisherModel>> GetPublisherAsync()
        {
            var publisher = await repository.GetPublishersAsync();
            var modelList = new List<PublisherModel>();
            foreach (var item in publisher) modelList.Add(MapToModel(item));
            return modelList;
        }

        /// <summary>
        /// Get a PublisherModel by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PublisherModel</returns>
        public async Task<PublisherModel> GetPublisherByIdAsync(int id)
        {
            var publisher = await repository.GetPublisherByIdAsync(id);
            if (publisher == null) return null;
            return MapToModel(publisher);
        }

        /// <summary>
        /// Get a PublisherModel by Name Async
        /// </summary>
        /// <param name="publisherName"></param>
        /// <returns>PublisherModel</returns>
        public async Task<PublisherModel> GetPublisherByNameAsync(string publisherName)
        {
            var publisher = await repository.GetPublisherByNameAsync(publisherName);
            if (publisher == null) return null;
            return MapToModel(publisher);
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a PublisherModel Async
        /// </summary>
        /// <param name="publisherModel"></param>
        /// <returns>PublisherModel</returns>
        public async Task<PublisherModel> UpdatePublisherAsync(PublisherModel publisherModel)
        {
            if (publisherModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdatePublisherAsync(MapToDbObject(publisherModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new PublisherModel Async
        /// </summary>
        /// <param name="publisherModel"></param>
        /// <returns>PublisherModel</returns>
        public async Task<PublisherModel> InsertPublisherAsync(PublisherModel publisherModel)
        {
            if (publisherModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertPublisherAsync(MapToDbObject(publisherModel)));
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Publisher by Id Async
        /// </summary>
        /// <param name="publisherId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeletePublisherByIdAsync(int publisherId)
        {
            return await repository.DeletePublisherByIdAsync(publisherId);
        }

        #endregion Delete Functions

        /// <summary>
        /// Map a DB Publishers Object to a PublisherModel
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns>PublisherModel</returns>
        private PublisherModel MapToModel(Publishers publisher)
        {
            if (publisher == null) return null;

            return new PublisherModel
            {
                Country = publisher.Country,
                Description = publisher.Description,
                Name = publisher.Name,
                PublisherId = publisher.PublisherId
            };
        }

        /// <summary>
        /// Map a PublisherModel to a DB Publishers Object
        /// </summary>
        /// <param name="publisherModel"></param>
        /// <returns>Publishers</returns>
        private Publishers MapToDbObject(PublisherModel publisherModel)
        {
            if (publisherModel == null) return null;

            return new Publishers
            {
                Country = publisherModel.Country,
                Description = publisherModel.Description,
                Name = publisherModel.Name,
                PublisherId = publisherModel.PublisherId
            };
        }
    }
}