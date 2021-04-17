using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.DataRepository
{
    static class ExtensionMethods
    {
        #region Vehicle
        public static VehicleTracking.DataRepository.Vehicle ToEntity(this VehicleTracking.Common.DTO.Vehicle dto)
        {
            var entity = new VehicleTracking.DataRepository.Vehicle();
            entity.RegistrationDate = dto.RegDate;
            entity.RegistrationNumber = dto.RegNumber;
            entity.VehicleID = dto.ID;
            return entity;
        }
        public static VehicleTracking.Common.DTO.Vehicle ToDto(this VehicleTracking.DataRepository.Vehicle entity)
        {
            var dto = new VehicleTracking.Common.DTO.Vehicle();
            dto.ID = entity.VehicleID;
            dto.RegDate = entity.RegistrationDate;
            dto.RegNumber = entity.RegistrationNumber;
            return dto;
        }
        #endregion

        #region Tracking
        public static VehicleTracking.DataRepository.Tracking ToEntity(this VehicleTracking.Common.DTO.Tracking dto)
        {
            var entity = new VehicleTracking.DataRepository.Tracking();
            entity.TrackingID = dto.ID;
            entity.VehicleID = dto.VehicleID;
            return entity;
        }
        public static VehicleTracking.Common.DTO.Tracking ToDto(this VehicleTracking.DataRepository.Tracking entity)
        {
            var dto = new VehicleTracking.Common.DTO.Tracking();
            dto.ID = entity.TrackingID;
            dto.VehicleID = entity.VehicleID;
            return dto;
        }
        #endregion

    }
}
