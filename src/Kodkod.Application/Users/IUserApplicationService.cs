using System.Collections.Generic;
using System.Threading.Tasks;
using Kodkod.Core;
using Kodkod.Core.Entities;

namespace Kodkod.Application.Users
{
    public interface IUserApplicationService
    {
        Task<List<ApplicationUser>> GetAllAsync();
    }
}