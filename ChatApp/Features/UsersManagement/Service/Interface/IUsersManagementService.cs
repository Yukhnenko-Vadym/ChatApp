using ChatApp.Features.UserAuth.Models;
using ChatApp.Features.UserManagement.Models.Requests;

namespace ChatApp.Features.UserManagement.Service.Interface;

public interface IUsersManagementService
{
    Task<User> Get(Guid id);
    Task<User> Update(Guid id, UpdateUserBody userBody);
    Task UpdatePassword(Guid id, string password);
    Task Delete(Guid id);
}