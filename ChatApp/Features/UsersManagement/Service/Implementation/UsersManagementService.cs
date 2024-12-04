using ChatApp.Features.UserAuth.Models;
using ChatApp.Features.UserManagement.Models.Requests;
using ChatApp.Features.UserManagement.Service.Interface;
using ChatApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Features.UserManagement.Service.Implementation;

public class UsersManagementService : IUsersManagementService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    
    public UsersManagementService(IUsersRepository usersRepository, IPasswordHasher<User> passwordHasher)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<User> Get(Guid id)
    {
        var user = await _usersRepository.Get(id);

        if (user == null)
        {
            throw new ArgumentException("User with this id not found");
        }

        return user;
    }

    public async Task<User> Update(Guid id, UpdateUserBody userBody)
    {
        var user = await _usersRepository.Get(id);

        if (user == null)
        {
            throw new ArgumentException("User with this id not found");
        }
        
        if (user.Email != userBody.Email && await _usersRepository.IsExist(userBody.Email))
        {
            throw new ArgumentException("User with this email already exist");
        }

        user.Update(userBody.Username, userBody.Email);

        await _usersRepository.Update(user);

        return user;
    }

    public async Task UpdatePassword(Guid id, string password)
    {
        var user = await _usersRepository.Get(id);

        if (user == null)
        {
            throw new ArgumentException("User with this id not found");
        }

        user.UpdatePassword(_passwordHasher.HashPassword(user, password));
        
        await _usersRepository.Update(user);
    }

    public async Task Delete(Guid id)
    {
        var user = await _usersRepository.Get(id);

        if (user == null)
        {
            throw new ArgumentException("User with this id not found");
        }
        
        await _usersRepository.Delete(user);
    }
}