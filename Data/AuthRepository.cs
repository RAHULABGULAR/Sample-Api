using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Sample_Api.Models;

namespace Sample_Api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> Exists(string userName)
        {
            if(await _context.Users.AnyAsync(c=>c.UserName.ToLower()==userName.ToLower()))
            return true;
            else
            return false;
        }

        public Task<ServiceResponse<string>> Login(User userNmae, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> objServiceResponse=new ServiceResponse<int>();
            if(await Exists(userName:user.UserName)){
                    objServiceResponse.success=false;
                    objServiceResponse.message="User "+user.UserName+" Already Exists.";
            }
            else{
            CreateHashPassword(password:password,out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            objServiceResponse.data=user.Id;
            }
            return objServiceResponse;
        }

        private void CreateHashPassword(string password,out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac=new HMACSHA512()){
                passwordHash=hmac.Key;  
                passwordSalt=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}