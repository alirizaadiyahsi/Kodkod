using Kodkod.EntityFramework;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kodkod.Web.Api.ActionFilters
{
    public class KodkodDbContextActionFilter : IActionFilter
    {
        private readonly KodkodDbContext _context;

        public KodkodDbContextActionFilter(KodkodDbContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _context.SaveChanges();
        }
    }
}