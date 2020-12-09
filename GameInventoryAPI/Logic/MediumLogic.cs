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
    public class MediumLogic
    {
        private readonly MediumRepository repository = new MediumRepository();

        public async Task<IEnumerable<MediumModel>> GetMediumsAsync()
        {
            var mediums = await repository.GetMediumsAsync();
            var modeList = new List<MediumModel>();
            foreach (var item in mediums) modeList.Add(MapToModel(item));
            return modeList;
        }

        public async Task<MediumModel> GetMediumByIdAsync(int id)
        {
            var medium = await repository.GetMediumByIdAsync(id);
            if (medium == null) return null;
            return MapToModel(medium);
        }

        private MediumModel MapToModel(Medium medium)
        {
            if (medium == null) return null;

            return new MediumModel
            {
                MediumId = medium.MediumId,
                Name = medium.Name
            };
        }
    }
}