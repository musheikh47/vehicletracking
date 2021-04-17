using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Common.Interfaces
{
    public interface IDataRepository<T> : IDisposable where T : class
    {
        Task<int> Create(T objectToCreate);
        Task<bool> Update(T objectToUpdate);
        Task<bool> Delete(T objectToDelete);
    }
}
