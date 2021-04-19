using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common.DTO
{
    public class Tracking
    {
        public int ID { get; set; }
        public int VehicleID { set; get; }

        [Range(-180,180,ErrorMessage = "Long should be between -180 to 180 degree.")]
        public double Long { set; get; }

        [Range(-90, 90, ErrorMessage = "Lat should be between -90 to 90 degree.")]
        public double Lat { set; get; }

        public long Time { set; get; }
    }
}
