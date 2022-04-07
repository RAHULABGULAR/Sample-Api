using Sample_Api.Models;

namespace Sample_Api.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register(User user,string password);
         Task<ServiceResponse<string>> Login(string userNmae,string password);
         Task<bool> Exists(string userName);
    }
}