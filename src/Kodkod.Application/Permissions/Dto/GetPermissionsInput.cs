using Kodkod.Application.Dto;

namespace Kodkod.Application.Permissions.Dto
{
    public class GetPermissionsInput : PagedListInput
    {
        public GetPermissionsInput()
        {
            Sorting = "Name";
        }
    }
}