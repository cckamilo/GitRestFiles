using System;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Interfaces;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using FilesApi.Utilities.Response;

namespace FilesApi.Business.Implementation
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserRepository repository;
        private readonly IConfiguration configuration;
        private readonly UserResponse response;


        public AuthenticationManager(IConfiguration _configuration, IUserRepository _respository, UserResponse _response)
        {
            this.repository = _respository;
            this.configuration = _configuration;
            this.response = _response;
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserResponse Authenticate(string username, string password)
        {

            var result =  repository.SearchForAsync(x => x.userName == username && x.password == password);

            if (result.Count > 0)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(configuration["Authentication:key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {

                        new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.token = tokenHandler.WriteToken(token);
                response.userId = result.Select(x => x.id).FirstOrDefault();
                response.role = result.Select(x => x.role).FirstOrDefault();
                return response;                      
            }
            else
            {
                return null;
            }
        }
    }
}
