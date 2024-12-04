using ChatApp.Features.UserAuth.Models;

namespace ChatApp.Repositories.Interfaces;

public interface IUsersRepository: IRepository<User>
{
    Task<bool> IsExist(string email);
    Task<User?> Get(string email);
}