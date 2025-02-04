using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;//if return >0 means something was saved
        }

        public void Update(AppUser user)//dont need async to void methods
        {
            //not very useful but when we modify user entity we can tell EF that the state was modified
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
