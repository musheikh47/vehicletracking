using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.Interfaces;

namespace VehicleTracking.DataRepository.Repositories
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
     
        #region Constructor
        public VehicleRepository()
        {

        }
        public VehicleRepository(VehicleTrackingDBEntities dbContext) : base(dbContext)
        {

        }
        #endregion
        #region IVehicle Repository
        public async Task<int> Create(Common.DTO.Vehicle objectToCreate)
        {
            var entity = objectToCreate.ToEntity();
            DbContext.Vehicles.Add(entity);
            await DbContext.SaveChangesAsync();
            return entity.VehicleID;
        }
        public async Task<bool> Delete(Common.DTO.Vehicle objectToDelete)
        {
            var flag = false;
            var entity = DbContext.Vehicles.FirstOrDefault(x => x.VehicleID == objectToDelete.ID);
            if (entity != null)
            {
                DbContext.Vehicles.Remove(entity);
                await DbContext.SaveChangesAsync();
                flag = true;
            }
            else
            {
                throw new ArgumentException($"Vehicle doesn't exist with ID {objectToDelete.ID}");    
            }

            return flag;

        }
        public async Task<bool> Update(Common.DTO.Vehicle objectToUpdate)
        {
            var flag = false;
            var entity = DbContext.Vehicles.FirstOrDefault(x => x.VehicleID == objectToUpdate.ID);
            if (entity != null)
            {
                entity.RegistrationDate = objectToUpdate.RegDate;
                entity.RegistrationNumber = objectToUpdate.RegNumber;
                await DbContext.SaveChangesAsync();
                flag = true;
            }
            else
            {
                throw new ArgumentException($"Vehicle doesn't exist with ID {objectToUpdate.ID}");
            }

            return flag;
        }
        #endregion
    }
}
