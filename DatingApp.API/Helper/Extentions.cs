
using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helper

{
    public static class Extentions 
    {
        public static void AddApplicationError(this HttpResponse response, string Message){
            response.Headers.Add("Application-Error",Message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");

        }

        public static int calculateAge (this DateTime DateOfBirth){

            var age =DateTime.Today.Year - DateOfBirth.Year;
            if(DateOfBirth.AddYears(age)>DateTime.Today) age--;
            return age;

        }
    }
}