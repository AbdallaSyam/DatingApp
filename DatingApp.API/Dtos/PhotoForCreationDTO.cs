using System;
using System.Collections.Generic;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos

{
    public class PhotoForCreationDTO
    {
      

        public string Url { get; set; }

         public IFormFile File { get; set; }

         public String PublicId { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public PhotoForCreationDTO()
        {
            DateAdded=DateTime.Now;
        }

       

        
    }
}