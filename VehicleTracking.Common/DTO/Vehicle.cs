using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common.DTO
{
    // The purpose of making Data transfer object is to isolate the complexity of EFModel from core logic
    public class Vehicle
    {
        // The name of the properties are small on purpose. These properties will travel over the network. Small name will help us to reduce the requst size

        public int ID { get; set; }
        public string RegNumber { get; set; } 
        public long RegDate { get; set; }
    }
}
