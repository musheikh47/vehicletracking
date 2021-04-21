using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace VehicleTrackingApi.Models
{
    public static class JWTHelper
    {
        public static string GenerateJWTToken(string issuer, string secret,string identity,string name,string role)
        {
            // This peice of code is copied from the internet
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[4];
            claims[0] = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims[1] = new Claim(ClaimTypes.Role,role);
            claims[2] = new Claim(JwtRegisteredClaimNames.Sub, identity);
            claims[3] = new Claim(JwtRegisteredClaimNames.Name, name);

            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            claims,
                            expires: DateTime.Now.AddDays(10), // This claim will be valid for 10 days. For this particular scenarion we can either extend to expiry of the claim or we can expose the mechanism to refresh the token when it is expired. 
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
    }
}