using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class PhotosController : ControllerBase
    {
        [Authorize] //this service will be authorize by token configuerd in configuration section in startup.cs
        [Route]     //ServiceURL
        
    }
}