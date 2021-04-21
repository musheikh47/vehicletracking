using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.DTO;

namespace VehicleTracking.Common.Interfaces
{
    public interface IUserRepository : IDataRepository<User>
    {
        Task<User> Authenticate(User user);
    }
}
