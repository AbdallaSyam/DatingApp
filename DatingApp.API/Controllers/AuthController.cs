using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo ;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _repo=repo;
            _config=config;
        }


        [HttpPost("register")]
        public async Task <IActionResult> Register( UserForRegisterDTOO UserToCreate ){

            //chk if user exist 

             UserToCreate.Username=UserToCreate.Username.ToLower();
                var IsUserExist =await _repo.UserExist(UserToCreate.Username);
             if(IsUserExist){
                 return BadRequest("UserExist");

             }
          var userTobecreated = new User{

              UserName=UserToCreate.Username
          };
             
          var CreatedUser = await _repo.Register(userTobecreated,UserToCreate.Password);
          return StatusCode(201);
        }
        [HttpPost("Login")]
            public async Task <IActionResult> Login( UserForLoginDTO UserForLoginDTO)
            {

                throw new Exception("Computer Says No");

         var User =await _repo.Login(UserForLoginDTO.Username,UserForLoginDTO.Password);
         if(User==null){
             return Unauthorized();
         }

         else{

             var Claims=new[]
             {
                 new Claim(ClaimTypes.NameIdentifier,User.Id.ToString()),
                 new Claim(ClaimTypes.Name, User.UserName)
             };

             var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));

             var Cred = new SigningCredentials(Key,SecurityAlgorithms.HmacSha512);
             
             var tokenDescriptor = new SecurityTokenDescriptor {
                 Subject= new ClaimsIdentity(Claims),
                 SigningCredentials=Cred,
                 Expires= DateTime.Now.AddDays(1)
             };

             var TokenHandller = new JwtSecurityTokenHandler();
             var Token = TokenHandller.CreateToken(tokenDescriptor);

             return Ok ( new {
                 Token= TokenHandller.WriteToken(Token)
             });

         }


    }


    }
}