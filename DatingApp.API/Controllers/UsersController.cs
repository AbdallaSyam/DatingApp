using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDateRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDateRepository repo, IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }
            [HttpGet("GetAllUsers")]
               public async Task <IActionResult> GetUsers (){
          var users = await _repo.GetUser();
          var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);
          return Ok(usersToReturn);

       }

      [HttpGet("GetAUser")]
        
       public async Task <IActionResult> GetUsers (int id){
          var user = await _repo.GetUser(id);
          var userToReturn = _mapper.Map<UserForDetaildDTO>(user);
          return Ok(userToReturn);

       }

    }
}