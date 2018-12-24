using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDateRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            
            var users =await _context.Users.Include(p=>p.Photo).Where(i => i.Photo.Count>0).ToListAsync();
            return users;
        }

        public async Task <User> GetUser(int id)
        {
            var user =await _context.Users.Include(p=>p.Photo).FirstOrDefaultAsync(u=>u.Id==id);
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0 ; 
        }
    }
}