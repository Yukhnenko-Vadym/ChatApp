using AutoMapper;
using ChatApp.Features.UserAuth.Models;
using ChatApp.Features.UsersManagement.Models.ViewModel;

namespace ChatApp.Mapping;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        AllowNullCollections = true;

        CreateMap<User, UserVm>();
    }
}