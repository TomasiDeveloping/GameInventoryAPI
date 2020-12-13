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

        #region Get Functions
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

        public async Task<PlattformModel> GetPlattformByNameAsync(string plattformName)
        {
            var plattform = await repository.GetPlattformByNameAsync(plattformName);
            if (plattform == null) return null;
            return MapToModel(plattform);
        }
        #endregion

        #region Update Functions
        public async Task<PlattformModel> UpdatePlattformAsync(PlattformModel plattformModel)
        {
            if (plattformModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdatePlattformAsync(MapToDbObject(plattformModel)));
        }
        #endregion

        #region Insert Functions
        public async Task<PlattformModel> InsertPlattformAsync(PlattformModel plattformModel)
        {
            if (plattformModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertPlattformAsync(MapToDbObject(plattformModel)));
        }
        #endregion

        #region Delete Functions
        public async Task<bool> DeletePlattformByIdAsync(int plattformId)
        {
            return await repository.DeletePlattformByIdAsync(plattformId);
        }

        public async Task<bool> ForceDeletePlattformByIdAsync(int plattformId)
        {
            return await repository.ForceDeletePlattformByIdAsync(plattformId);
        }
        #endregion

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

        private Plattform MapToDbObject(PlattformModel plattformModel)
        {
            if (plattformModel == null) return null;

            return new Plattform
            {
                Description = plattformModel.Description,
                Name = plattformModel.Name,
                PlattformId = plattformModel.PlattformId,
                Producer = plattformModel.Producer,
                Release = plattformModel.Release
            };
        }
    }
}