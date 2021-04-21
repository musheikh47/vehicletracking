using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common;
using VehicleTracking.Common.DTO;
using VehicleTracking.Common.Interfaces;

namespace VehicleTracking.Engine
{
    public class AccountManager : IDisposable
    {
        #region Private Properties
        private IUserRepository _userRepository;
        #endregion

        #region Constructor
        public AccountManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Public Methods
        public async Task<User> Authenticate(User user)
        {
            try
            {
                user.Password = ComputeSha256Hash(user.Password);
                return await _userRepository.Authenticate(user);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,$"Error accured while authenticating user {user.Username}");
                throw;
            }
        }
        #endregion

        #region Private Methods
        private string ComputeSha256Hash(string rawData)
        {
            // This peice of code is copied from the internet
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion

        #region IDisposible Implementation
        public void Dispose()
        {
            _userRepository?.Dispose();
        }
        #endregion
    }
}
