using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common.DTO
{
    public class TrackingSearcher
    {
        private const double MIN_DATE = 0; // DateTime.MinValue.Ticks
        private const double MAX_DATE = 3155378975999999999; // DateTime.MaxValue.Ticks
        public int VehicleID { get; set; }

        [Range(MIN_DATE,MAX_DATE,ErrorMessage = "Invalid StartTime. StartTime should be between 0 to 3155378975999999999")] 
        public long StartTime { get; set; }

        [Range(MIN_DATE, MAX_DATE, ErrorMessage = "Invalid EndTime. EndTime should be between 0 to 3155378975999999999")]
        public long EndTime { get; set; }
    }
}
