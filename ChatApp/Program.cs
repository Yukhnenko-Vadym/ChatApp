using ChatApp.Data;
using ChatApp.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ChatApp.Features.UserAuth.Models;
using ChatApp.Features.UserAuth.Service.Implementation;
using ChatApp.Features.UserAuth.Service.Interface;
using ChatApp.Features.UserManagement.Service.Implementation;
using ChatApp.Features.UserManagement.Service.Interface;
using ChatApp.Mapping;
using ChatApp.Repositories.Implementations;
using ChatApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.AddControllers(opts => opts.Filters.Add<ApiExceptionFilter>());  

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ChatContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.RequireHttpsMetadata = false;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]!)),
        ValidateIssuer = true,
        ValidIssuer = config["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = config["JWT:ValidAudience"],
        ClockSkew = TimeSpan.Zero
    };
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddTransient<IUsersRepository, UsersRepository>();

services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddTransient<IAuthService, AuthService>();
services.AddTransient<IUsersManagementService, UsersManagementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

