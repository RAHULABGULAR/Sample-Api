using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sample_Api.Models;

namespace Sample_Api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;

        }
        public async Task<bool> Exists(string userName)
        {
            if (await _context.Users.AnyAsync(c => c.UserName.ToLower() == userName.ToLower()))
                return true;
            else
                return false;
        }

        public async Task<ServiceResponse<string>> Login(string userNmae, string password)
        {
            ServiceResponse<string> resp = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userNmae.ToLower());
            if (user != null)
            {
                if (VerifyPasswordHash(password: password, passwordHash: user.PasswordHash, passwordSalt: user.PasswordSalt))
                {
                    resp.data = CreateToken(user:user);
                }
                else
                {
                    resp.success = false;
                    resp.message = "Incorrect Password";
                }
            }
            else
            {
                resp.success = false;
                resp.message = "Incorrect User Name";
            }
            return resp;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> objServiceResponse = new ServiceResponse<int>();
            if (await Exists(userName: user.UserName))
            {
                objServiceResponse.success = false;
                objServiceResponse.message = "User " + user.UserName + " Already Exists.";
            }
            else
            {
                CreateHashPassword(password: password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                objServiceResponse.data = user.Id;
            }
            return objServiceResponse;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s: password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>{
            new Claim(type:ClaimTypes.NameIdentifier,value:user.Id.ToString()),
            new Claim(type:ClaimTypes.Name,value:user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection(key:"AppSettings:Token").Value));

            var creds=new SigningCredentials(key:key,algorithm: SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims:claims),
                Expires=DateTime.Now.AddMinutes(3),
                SigningCredentials=creds
            };

            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor:tokenDescriptor);

            return tokenHandler.WriteToken(token:token);
        }
    }
}