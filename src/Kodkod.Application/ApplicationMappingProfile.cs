using AutoMapper;
using Kodkod.Application.Permissions.Dto;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;

namespace Kodkod.Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<User, UserListDto>();
            CreateMap<Permission, PermissionListDto>();
            //todo: add auto mapper mapping configurations here
        }
    }
}