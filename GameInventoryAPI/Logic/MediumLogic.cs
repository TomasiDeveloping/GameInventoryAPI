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

        #region Get Functions
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

        public async Task<MediumModel> GetMediumByNameAsync(string mediumName)
        {
            var medium = await repository.GetMediumByNameAsync(mediumName);
            if (medium == null) return null;
            return MapToModel(medium);
        }
        #endregion

        #region Update Functions
        public async Task<MediumModel> UpdateMediumAsync(MediumModel mediumModel)
        {
            if (mediumModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateMediumAsync(MapToDbObject(mediumModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<MediumModel> InsertMediumAsync(MediumModel mediumModel)
        {
            if (mediumModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertMediumAsync(MapToDbObject(mediumModel)));
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeleteMediumByIdAsync(int mediumId)
        {
            return await repository.DeleteMediumByIdAsync(mediumId);
        }

        public async Task<bool> ForceDeleteMediumByIdAsync(int mediumId)
        {
            return await repository.ForceDeleteMediumByIdAsync(mediumId);
        }
        #endregion

        private MediumModel MapToModel(Medium medium)
        {
            if (medium == null) return null;

            return new MediumModel
            {
                MediumId = medium.MediumId,
                Name = medium.Name
            };
        }

        private Medium MapToDbObject(MediumModel mediumModel)
        {
            if (mediumModel == null) return null;

            return new Medium
            {
                MediumId = mediumModel.MediumId,
                Name = mediumModel.Name
            };
        }
    }
}