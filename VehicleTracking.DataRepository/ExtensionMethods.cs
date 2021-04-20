using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
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
            entity.TrackingTime = dto.Time;
            entity.Location = DbGeography.PointFromText($"Point({dto.Long} {dto.Lat})", 4326); // 4326 represents WGS84 Datum projection system. Google Map uses this projection system. Usage of Geography type will open possibilities to execute geographical queries directly on database.
            return entity;
        }
        public static VehicleTracking.Common.DTO.Tracking ToDto(this VehicleTracking.DataRepository.Tracking entity)
        {            
            var dto = new VehicleTracking.Common.DTO.Tracking();
            dto.ID = entity.TrackingID;
            dto.VehicleID = entity.VehicleID;
            dto.Time = entity.TrackingTime;
            dto.Long = entity.Location.Longitude.Value;
            dto.Lat = entity.Location.Latitude.Value;
            return dto;
        }
        #endregion

    }
}
