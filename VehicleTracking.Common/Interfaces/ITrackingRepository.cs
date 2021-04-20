using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.DTO;

namespace VehicleTracking.Common.Interfaces
{
    public interface ITrackingRepository : IDataRepository<DTO.Tracking>
    {
        /// <summary>
        /// Ths method will return last recorded tracking entry
        /// </summary>
        /// <param name="identity">Identity can be vehicleID or registration number. </param>
        /// <returns></returns>
        Task<Tracking> GetLastTrackingEntry(int vehicleID);

        /// <summary>
        /// This method will search tracking records using given interval and vehicle identiy
        /// </summary>
        /// <param name="searcher"></param>
        /// <returns></returns>
        Task<SearchResult> Search(TrackingSearcher searcher);
    }
}
