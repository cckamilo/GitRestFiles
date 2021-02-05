using System;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Interfaces;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace FilesApi.Business.Implementation
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserRepository repository;
        private readonly IConfiguration configuration;


        public AuthenticationManager(IConfiguration _configuration, IUserRepository _respository)
        {
            this.repository = _respository;
            this.configuration = _configuration;
           
        }

        public string Authenticate(string username, string password)
        {

            var result = repository.SearchForAsync(x => x.userName == username && x.password == password);

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
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return null;
            }
        }
    }
}
