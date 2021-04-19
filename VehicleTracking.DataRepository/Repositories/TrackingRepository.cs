using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.Interfaces;

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
            var entity = DbContext.Trackings.FirstOrDefault(x => x.TrackingID == objectToDelete.ID);
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
        public async Task<bool> Update(Common.DTO.Tracking objectToUpdate)
        {
            var flag = false;
            var entity = DbContext.Trackings.FirstOrDefault(x => x.TrackingID == objectToUpdate.ID);
            if (entity != null)
            {
                // TODO
                await DbContext.SaveChangesAsync();
                flag = true;
            }
            else
            {
                throw new ArgumentException($"Tracking Entry doesn't exist with ID {objectToUpdate.ID}");
            }

            return flag;
        }

        #endregion

    }
}
