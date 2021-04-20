using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common.DTO
{
    public class SearchResult
    {
        public int VehicleID { get; set; }

        /// <summary>
        /// Path will contain sorted Tracking records.
        /// </summary>
        public Tracking[] Path { set; get; }
    }
}
