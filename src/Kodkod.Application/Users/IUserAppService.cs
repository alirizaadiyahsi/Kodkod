﻿using System.Threading.Tasks;
using Kodkod.Application.Users.Dto;
using Kodkod.Utilities.PagedList;

namespace Kodkod.Application.Users
{
    public interface IUserAppService
    {
        Task<IPagedList<UserListDto>> GetUsersAsync(UserListInput input);
    }
}