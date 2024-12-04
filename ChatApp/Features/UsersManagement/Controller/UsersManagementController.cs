using AutoMapper;
using ChatApp.Features.UserManagement.Models.Requests;
using ChatApp.Features.UserManagement.Service.Interface;
using ChatApp.Features.UsersManagement.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Features.UsersManagement.Controller;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UsersManagementController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUsersManagementService _usersManagementService;

    public UsersManagementController(IMapper mapper, IUsersManagementService usersManagementService)
    {
        _mapper = mapper;
        _usersManagementService = usersManagementService;
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        return Ok(_mapper.Map<UserVm>(await _usersManagementService.Get(id)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserBody userBody)
    {
        return Ok(_mapper.Map<UserVm>(await _usersManagementService.Update(id, userBody)));
    }

    [HttpPut("{id}/password")]
    public async Task<IActionResult> UpdateUserPassword([FromRoute] Guid id, [FromBody] UpdateUserPasswordBody userPasswordBody)
    {
        await _usersManagementService.UpdatePassword(id, userPasswordBody.Password);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        await _usersManagementService.Delete(id);

        return Ok();
    }
}