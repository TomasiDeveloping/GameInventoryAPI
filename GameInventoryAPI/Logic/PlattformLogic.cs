using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class PlattformLogic
    {
        private readonly PlattformRepository repository = new PlattformRepository();

        #region Get Functions

        /// <summary>
        /// Get all PlattformModel Async
        /// </summary>
        /// <returns>IEnumerable of PlattformModel</returns>
        public async Task<IEnumerable<PlattformModel>> GetPlattformsAsync()
        {
            var plattforms = await repository.GetPlattformsAsync();
            var modelList = new List<PlattformModel>();
            foreach (var item in plattforms) modelList.Add(MapToModel(item));
            return modelList;
        }

        /// <summary>
        /// Get a PlattformModel by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PlattformModel</returns>
        public async Task<PlattformModel> GetPlattformByIdAsync(int id)
        {
            var plattform = await repository.GetPlattformByIdAsync(id);
            if (plattform == null) return null;
            return MapToModel(plattform);
        }

        /// <summary>
        /// Get a PlattformModel by Name Async
        /// </summary>
        /// <param name="plattformName"></param>
        /// <returns>PlattformModel</returns>
        public async Task<PlattformModel> GetPlattformByNameAsync(string plattformName)
        {
            var plattform = await repository.GetPlattformByNameAsync(plattformName);
            if (plattform == null) return null;
            return MapToModel(plattform);
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a PlattformModel Async
        /// </summary>
        /// <param name="plattformModel"></param>
        /// <returns>PlattformModel</returns>
        public async Task<PlattformModel> UpdatePlattformAsync(PlattformModel plattformModel)
        {
            if (plattformModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdatePlattformAsync(MapToDbObject(plattformModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new PlattformModel Async
        /// </summary>
        /// <param name="plattformModel"></param>
        /// <returns>PlattformModel</returns>
        public async Task<PlattformModel> InsertPlattformAsync(PlattformModel plattformModel)
        {
            if (plattformModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertPlattformAsync(MapToDbObject(plattformModel)));
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Plattform by Id Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeletePlattformByIdAsync(int plattformId)
        {
            return await repository.DeletePlattformByIdAsync(plattformId);
        }

        /// <summary>
        /// Delete a Plattform by Id with all dependencies Async
        /// </summary>
        /// <param name="plattformId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeletePlattformByIdAsync(int plattformId)
        {
            return await repository.ForceDeletePlattformByIdAsync(plattformId);
        }

        #endregion Delete Functions

        /// <summary>
        /// Map a DB Plattform Object to a PlattformModel
        /// </summary>
        /// <param name="plattform"></param>
        /// <returns>PlattformModel</returns>
        private PlattformModel MapToModel(Plattform plattform)
        {
            if (plattform == null) return null;

            return new PlattformModel
            {
                Description = plattform.Description,
                Name = plattform.Name,
                PlattformId = plattform.PlattformId,
                Producer = plattform.Producer,
                Release = plattform.Release,
                PhotoUrl = plattform.PhotoUrl
            };
        }

        /// <summary>
        /// Map a PlattformModel to a DB Platfform Object
        /// </summary>
        /// <param name="plattformModel"></param>
        /// <returns>Plattform</returns>
        private Plattform MapToDbObject(PlattformModel plattformModel)
        {
            if (plattformModel == null) return null;

            return new Plattform
            {
                Description = plattformModel.Description,
                Name = plattformModel.Name,
                PlattformId = plattformModel.PlattformId,
                Producer = plattformModel.Producer,
                Release = plattformModel.Release,
                PhotoUrl = plattformModel.PhotoUrl
            };
        }
    }
}