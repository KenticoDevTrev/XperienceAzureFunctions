using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CMS.DocumentEngine.Types.Generic;
using CMS.DocumentEngine;
using System.Linq;
using CMS.Membership;

namespace XperienceAzureFunction
{
    public class GetNavigationItems
    {
        // Undid Static so i could use Dependency Injection
        public GetNavigationItems(IUserInfoProvider userInfoProvider)
        {
            UserInfoProvider = userInfoProvider;
        }

        public IUserInfoProvider UserInfoProvider { get; }

        [FunctionName("GetNavigationItems")]
        // Will retry 5 times with 2 seconds between if fails.
        [FixedDelayRetry(5, "00:00:02")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            GetNavigationItemsRequest GetNavRequest,
            HttpRequest req,
            ILogger log)
        {
            // Sample Logging
            log.LogInformation("Example of Dependency Injection with Kentico Interfaces: "+ UserInfoProvider.Get().FirstOrDefault().UserName);

            // Do Logic here
            var NavigationFieldItems = new DocumentQuery<Navigation>()
                .TopN(GetNavRequest.NumItems)
                .Select(x => new Navigation.NavigationFields(x));

            // Build your model and return it here.
            return new JsonResult(NavigationFieldItems);
        }
    }

    // Request Model, will map { "NumItems":5 } to this object.
    public class GetNavigationItemsRequest
    {
        public int NumItems { get; set; } = 100;
    }
}
