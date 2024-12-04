using ChatApp.Features.UserAuth.Models;
using ChatApp.Features.UserAuth.Models.Requests;
using ChatApp.Features.UserAuth.Service.Interface;
using ChatApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatApp.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace ChatApp.Features.UserAuth.Service.Implementation;

public class AuthService: IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IConfiguration configuration, IUsersRepository usersRepository, IPasswordHasher<User> passwordHasher)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<string> Login(LoginBody loginBody)
    {
        var user = await _usersRepository.Get(loginBody.Email);

        if (user == null)
        {
            throw new AuthenticationException("User with this email not found");
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, loginBody.Password);

        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new AuthenticationException("Provided wrong password.");
        }

        return GenerateToken(user);
    }

    public async Task<string> Register(RegisterBody registerBody)
    {
        if (await _usersRepository.IsExist(registerBody.Email))
        {
            throw new AuthenticationException("User with this email already exist");
        }

        var user = await _usersRepository.Create(new User(
            registerBody.Username,
            registerBody.Email,
            _passwordHasher.HashPassword(null, registerBody.Password)
        ));

        return GenerateToken(user);
    }
    
    private string GenerateToken(User user)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = new ClaimsIdentity(new []
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.Username)
            })
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}