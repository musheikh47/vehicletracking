using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
            try
            {
                var entity = objectToCreate.ToEntity();
                DbContext.Vehicles.Add(entity);
                await DbContext.SaveChangesAsync();
                return entity.VehicleID;
            }
            catch (DbUpdateException ex)
            {
                if (ex?.InnerException is UpdateException childEx && childEx.InnerException is SqlException sqlex && sqlex.Number == 2627) // Unique constraint error
                {
                    throw new ArgumentException($"Registration number {objectToCreate.RegNumber} already exists.");
                }
                else
                {
                    throw;
                }
            }
           
        }
        public async Task<bool> Delete(Common.DTO.Vehicle objectToDelete)
        {
            var flag = false;
            var entity = await DbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == objectToDelete.ID);
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
            var entity = await DbContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleID == objectToUpdate.ID);
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
