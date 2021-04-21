using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.Interfaces;

namespace VehicleTracking.DataRepository.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        #region Constructor
        public UserRepository()
        {

        }
        public UserRepository(VehicleTrackingDBEntities dbContext) : base(dbContext)
        {

        }
        #endregion

        #region IUserRepository
        public async Task<Common.DTO.User> Authenticate(Common.DTO.User user)
        {
            var entity = await DbContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == user.Username.ToLower() && x.Password == user.Password);
            if (entity == null)
            {
                return null;
            }
            user.Role = entity.Role;
            user.ID = entity.UserID;
            return user;
        }
        public Task<int> Create(Common.DTO.User objectToCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Common.DTO.User objectToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Common.DTO.User objectToUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
