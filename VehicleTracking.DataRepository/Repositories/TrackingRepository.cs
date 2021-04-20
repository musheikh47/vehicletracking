using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.Interfaces;
using System.Data.Entity;

namespace VehicleTracking.DataRepository.Repositories
{
    public class TrackingRepository : BaseRepository, ITrackingRepository
    {
        #region Constructor
        public TrackingRepository()
        {

        }
        public TrackingRepository(VehicleTrackingDBEntities dbContext) : base(dbContext)
        {

        }
        #endregion

        #region ITrackingRepository
        public async Task<int> Create(Common.DTO.Tracking objectToCreate)
        {
            var entity = objectToCreate.ToEntity();
            DbContext.Trackings.Add(entity);
            await DbContext.SaveChangesAsync();
            return entity.TrackingID;
        }

        public async Task<bool> Delete(Common.DTO.Tracking objectToDelete)
        {
            var flag = false;
            var entity = await DbContext.Trackings.FirstOrDefaultAsync(x => x.TrackingID == objectToDelete.ID);
            if (entity != null)
            {
                DbContext.Trackings.Remove(entity);
                await DbContext.SaveChangesAsync();
                flag = true;
            }
            else
            {
                throw new ArgumentException($"Tracking entry doesn't exist with ID {objectToDelete.ID}");
            }

            return flag;
        }

        public async Task<Common.DTO.Tracking> GetLastTrackingEntry(int vehicleID)
        {
            var entity = await DbContext.Trackings.OrderByDescending(x => x.TrackingID).FirstOrDefaultAsync(x => x.VehicleID == vehicleID);
            if (entity == null)
            {
                throw new ArgumentException($"Tracking record doesn't exist with VehicleID {vehicleID}");
            }
            return entity.ToDto();
        }

        /// <summary>
        /// This method will return empty result if vehicle doesn't exists or there is no tracking record during the given interval. 
        /// </summary>
        /// <param name="searcher"></param>
        /// <returns></returns>
        public async Task<Common.DTO.SearchResult> Search(Common.DTO.TrackingSearcher searcher)
        {
            var list = await DbContext.Trackings.OrderBy(x => x.TrackingID).Where(x => 
            x.VehicleID == searcher.VehicleID
            && x.TrackingTime >= searcher.StartTime 
            && x.TrackingTime <= searcher.EndTime
            ).ToListAsync();
            var result = new Common.DTO.SearchResult();
            result.Path = new Common.DTO.Tracking[list.Count];
            result.VehicleID = searcher.VehicleID;
            for (int i = 0; i < list.Count; i++)
            {
                result.Path[i] = list[i].ToDto();
            }
            return result;
        }

        public Task<bool> Update(Common.DTO.Tracking objectToUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
