using ChatApp.Features.UserAuth.Models.Requests;
using ChatApp.Features.UserAuth.Models.ViewModel;
using ChatApp.Features.UserAuth.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Features.UserAuth.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginBody loginBody)
    {
        return Ok(new AuthTokenVm
        {
            Token = await _authService.Login(loginBody)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterBody userBody)
    {
        return Ok(new AuthTokenVm
        {
            Token = await _authService.Register(userBody)
        });
    }
}