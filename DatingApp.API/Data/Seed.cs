using System.Collections.Generic;
using DatingApp.API.Model;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }
     private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using (var hmac= new  System.Security.Cryptography.HMACSHA512 () ) {

               passwordSalt=hmac.Key;
               passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

           }
        }
        public void SeedUsers(){
            var UserData = System.IO.File.ReadAllText("Model/UserSeedData.json");
            var users = JsonConvert.DeserializeObject< List <User>>(UserData);
            foreach(var User in users){

                byte[] passwordHash,passwordSalt;
                CreatePasswordHash("password",out passwordHash,out passwordSalt);
                User.PasswordHash=passwordHash;
                User.PasswordSalt=passwordSalt;
                User.UserName=User.UserName.ToLower();
                _context.Add(User);

            }
            _context.SaveChanges();
        }
    }
}