using System;
using System.Collections.Generic;
using DatingApp.API.Model;

namespace DatingApp.API.Dtos
{
    public class UserForDetaildDTO
    {
         public int Id { get; set; }
        public string username { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }
        public string knowsAs { get; set; }
        public DateTime Created { get; set; } 
         public DateTime LastActive { get; set; }
        public string introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

          public string photoUrl { get; set; }
         public ICollection <PhotoForDetailedDto> Photo { get; set; }
    }
}