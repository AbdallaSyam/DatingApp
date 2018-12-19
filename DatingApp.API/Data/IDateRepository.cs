using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public interface IDateRepository
    {
        void Add<T>(T entity) where T :class;

        void Delete <T>(T entity) where T :class;
        Task <bool> SaveAll ();

        Task  <User> GetUser (int id);

        Task  <IEnumerable<User>> GetUser ();
         
    }
}