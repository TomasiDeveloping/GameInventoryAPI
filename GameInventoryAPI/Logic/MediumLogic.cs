using GameInventoryAPI.Data;
using GameInventoryAPI.Entities;
using GameInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameInventoryAPI.Logic
{
    public class MediumLogic
    {
        private readonly MediumRepository repository = new MediumRepository();

        #region Get Functions

        /// <summary>
        /// Get all MediumModel Async
        /// </summary>
        /// <returns>IEnumerable of MediumModel</returns>
        public async Task<IEnumerable<MediumModel>> GetMediumsAsync()
        {
            var mediums = await repository.GetMediumsAsync();
            var modeList = new List<MediumModel>();
            foreach (var item in mediums) modeList.Add(MapToModel(item));
            return modeList;
        }

        /// <summary>
        /// Get a MediumModel by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MediumModel</returns>
        public async Task<MediumModel> GetMediumByIdAsync(int id)
        {
            var medium = await repository.GetMediumByIdAsync(id);
            if (medium == null) return null;
            return MapToModel(medium);
        }

        /// <summary>
        /// Get a MediumModel by Name Async
        /// </summary>
        /// <param name="mediumName"></param>
        /// <returns>MediumModel</returns>
        public async Task<MediumModel> GetMediumByNameAsync(string mediumName)
        {
            var medium = await repository.GetMediumByNameAsync(mediumName);
            if (medium == null) return null;
            return MapToModel(medium);
        }

        #endregion Get Functions

        #region Update Functions

        /// <summary>
        /// Update a MediumModel Async
        /// </summary>
        /// <param name="mediumModel"></param>
        /// <returns>MediumModel</returns>
        public async Task<MediumModel> UpdateMediumAsync(MediumModel mediumModel)
        {
            if (mediumModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.UpdateMediumAsync(MapToDbObject(mediumModel)));
        }

        #endregion Update Functions

        #region Insert Functions

        /// <summary>
        /// Insert a new MediumModel Async
        /// </summary>
        /// <param name="mediumModel"></param>
        /// <returns>MediumModel</returns>
        public async Task<MediumModel> InsertMediumAsync(MediumModel mediumModel)
        {
            if (mediumModel == null) throw new ArgumentException(Properties.Settings.Default.ObjectNullException);
            return MapToModel(await repository.InsertMediumAsync(MapToDbObject(mediumModel)));
        }

        #endregion Insert Functions

        #region Delete Functions

        /// <summary>
        /// Delete a Medium by Id Async
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteMediumByIdAsync(int mediumId)
        {
            return await repository.DeleteMediumByIdAsync(mediumId);
        }

        /// <summary>
        /// Delete a Medium by Id with all dependencies Async
        /// </summary>
        /// <param name="mediumId"></param>
        /// <returns>bool</returns>
        public async Task<bool> ForceDeleteMediumByIdAsync(int mediumId)
        {
            return await repository.ForceDeleteMediumByIdAsync(mediumId);
        }

        #endregion Delete Functions

        /// <summary>
        /// Map a DB Medium Pbject to a MediumModel
        /// </summary>
        /// <param name="medium"></param>
        /// <returns>MediumModel</returns>
        private MediumModel MapToModel(Medium medium)
        {
            if (medium == null) return null;

            return new MediumModel
            {
                MediumId = medium.MediumId,
                Name = medium.Name
            };
        }

        /// <summary>
        /// Map a MediumModel to a DB Medium Object
        /// </summary>
        /// <param name="mediumModel"></param>
        /// <returns>Medium</returns>
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