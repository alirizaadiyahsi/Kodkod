using Kodkod.Application.Dto;

namespace Kodkod.Application.Users.Dto
{
    public class GetUsersInput : PagedListInput
    {
        public GetUsersInput()
        {
            Sorting = "UserName";
        }
    }
}