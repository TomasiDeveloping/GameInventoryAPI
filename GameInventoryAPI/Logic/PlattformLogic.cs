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
    public class PlattformLogic
    {
        private readonly PlattformRepository repository = new PlattformRepository();

        public async Task<IEnumerable<PlattformModel>> GetPlattformsAsync()
        {
            var plattforms = await repository.GetPlattformsAsync();
            var modelList = new List<PlattformModel>();
            foreach (var item in plattforms) modelList.Add(MapToModel(item));
            return modelList;
        }

        public async Task<PlattformModel> GetPlattformByIdAsync(int id)
        {
            var plattform = await repository.GetPlattformByIdAsync(id);
            if (plattform == null) return null;
            return MapToModel(plattform);
        }

        private PlattformModel MapToModel(Plattform plattform)
        {
            if (plattform == null) return null;

            return new PlattformModel
            {
                Description = plattform.Description,
                Name = plattform.Name,
                PlattformId = plattform.PlattformId,
                Producer = plattform.Producer,
                Release = plattform.Release
            };
        }
    }
}