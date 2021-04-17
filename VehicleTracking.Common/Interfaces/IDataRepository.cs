using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common.Interfaces
{
    public interface IDataRepository<T>
    {
        int Create(T objectToCreate);
        bool Update(T objectToUpdate);
        bool Delete(T objectToDelete);
    }
}
