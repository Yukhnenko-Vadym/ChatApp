using ChatApp.Features.UserAuth.Models.Requests;

namespace ChatApp.Features.UserAuth.Service.Interface;

public interface IAuthService
{
    Task<string> Login(LoginBody loginBody);
    Task<string> Register(RegisterBody registerBody);
}