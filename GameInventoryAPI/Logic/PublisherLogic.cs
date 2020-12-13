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
    public class PublisherLogic
    {
        private readonly PublisherRepository repository = new PublisherRepository();

        #region Get Functions
        public async Task<IEnumerable<PublisherModel>> GetPublisherAsync()
        {
            var publisher = await repository.GetPublishersAsync();
            var modelList = new List<PublisherModel>();
            foreach (var item in publisher) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<PublisherModel> GetPublisherByIdAsync(int id)
        {
            var publisher = await repository.GetPublisherByIdAsync(id);
            if (publisher == null) return null;
            return MapToModel(publisher);
        }

        public async Task<PublisherModel> GetPublisherByNameAsync(string publisherName)
        {
            var publisher = await repository.GetPublisherByNameAsync(publisherName);
            if (publisher == null) return null;
            return MapToModel(publisher);
        }
        #endregion

        #region Update Functions
        public async Task<PublisherModel> UpdatePublisherAsync(PublisherModel publisherModel)
        {
            if (publisherModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdatePublisherAsync(MapToDbObject(publisherModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<PublisherModel> InsertPublisherAsync(PublisherModel publisherModel)
        {
            if (publisherModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertPublisherAsync(MapToDbObject(publisherModel)));
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeletePublisherByIdAsync(int publisherId)
        {
            return await repository.DeletePublisherByIdAsync(publisherId);
        }
        #endregion

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