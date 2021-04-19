using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        [Required(ErrorMessage = "RegNumber is required.")]
        [MaxLength(50,ErrorMessage = "RegNumber should be less than or equal to 50 characters.")]
        public string RegNumber { get; set; } 
        public long RegDate { get; set; }
    }
}
