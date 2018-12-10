using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForLoginDTO
    {
        [Required]
        public string Username {get ; set;}

        [Required]

        public string Password  {set;get; }
    }
}