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
    }
}