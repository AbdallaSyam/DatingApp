using System;
using System.Data;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
         private readonly DataContext _Context;
         public AuthRepository(DataContext context)
        {
            _Context = context;
        }
      

      public async Task <User> Login (string Username,string Password) {

          var user = await _Context.Users.FirstOrDefaultAsync(x=>x.UserName==Username);

          if (user==null){
              return null;
          }
          if (!VerifyPasswordHash(Password,user.PasswordHash,user.PasswordSalt)){
              return null;
          }
          else{
              return user;
          }

   
      }

       
        public async Task <User> Register (User User,string Password) {

          byte[] PasswordHash;
          byte[] PasswordSalt;

          CreatePasswordHash(Password,out PasswordHash, out PasswordSalt);

          User.PasswordHash = PasswordHash;
          User.PasswordSalt = PasswordSalt;
          await _Context.Users.AddAsync(User);


try
{
await _Context.SaveChangesAsync();
}
catch(DataException DE)
{

}
catch(Exception DE)
{

}
          

          return User;
      }

     
        public async Task <bool> UserExist (string Username) {

        return await _Context.Users.AnyAsync(x=>x.UserName==Username);
      
      }


    #region Helper methods 
        
   //     CreatePasswordHash & CreatePasswordSalt
         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using (var hmac= new  System.Security.Cryptography.HMACSHA512 () ) {

               passwordSalt=hmac.Key;
               passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

           }
        }

        // Decode Hash Password
         private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac= new  System.Security.Cryptography.HMACSHA512 (passwordSalt) ) {

           var ComputeHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

           for(int i =0; i<ComputeHash.Length;i++){

               if(ComputeHash[i]!=passwordHash[i]) return false;
           }

        }
        return true;
        }
        #endregion
    }




    
}