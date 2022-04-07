using Microsoft.AspNetCore.Mvc;
using Sample_Api.Data;
using Sample_Api.Dtos;
using Sample_Api.Models;

namespace Sample_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<List<int>>>> Register(RegisterUserDto objRegisterUserDto){
        var response= await _repository.Register(user:new User{UserName= objRegisterUserDto.UserName},password:objRegisterUserDto.Password);
        if(!response.success)
        return BadRequest(response);
        return Ok(response);
        }
        
           [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> Login(LoginUserDto objLoginUserDto){
        var response= await _repository.Login(userNmae:objLoginUserDto.UserName,password:objLoginUserDto.Password);
        if(!response.success)
        return BadRequest(response);
        return Ok(response);
        }
    }
}