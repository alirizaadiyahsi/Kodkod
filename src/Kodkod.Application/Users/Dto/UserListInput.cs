using Kodkod.Application.Dto;

namespace Kodkod.Application.Users.Dto
{
    public class UserListInput : PagedListInput
    {
        public UserListInput()
        {
            Sorting = "UserName";
        }
    }
}