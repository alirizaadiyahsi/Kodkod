using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kodkod.Web.Core.Helpers
{
    public static class ErrorHelper
    {
        public static ModelStateDictionary AddErrorsToModelState(IdentityResult identityResult, ModelStateDictionary modelState)
        {
            foreach (var e in identityResult.Errors)
            {
                modelState.TryAddModelError(e.Code, e.Description);
            }

            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string key, string description, ModelStateDictionary modelState)
        {
            modelState.TryAddModelError(key, description);

            return modelState;
        }
    }
}
