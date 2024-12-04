using ChatApp.Data;
using ChatApp.Features.UserAuth.Models;
using ChatApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repositories.Implementations;

public class UsersRepository: Repository<User>, IUsersRepository
{
    public UsersRepository(ChatContext dbContext) : base(dbContext)
    {
    }

    public override async Task<User?> Get(Guid id)
    {
        //get list of chats ant other stuff by id with using Include
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> IsExist(string email)
    {
        return await _dbSet.AnyAsync(user => user.Email == email);
    }

    public async Task<User?> Get(string email)
    {
        //get list of chats ant other stuff by id with using Include
        return await _dbSet.FirstOrDefaultAsync(user => user.Email == email);
    }
}