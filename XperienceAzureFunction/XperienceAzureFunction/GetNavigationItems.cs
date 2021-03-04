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
    public static class GetNavigationItems
    {
        [FunctionName("GetNavigationItems")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            GetNavigationItemsRequest GetNavRequest,
            IUserInfoProvider userInfoProvider,
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Example of Dependency Injection with Kentico Interfaces: "+ userInfoProvider.Get().FirstOrDefault().UserName);

            // Do Logic here
            var NavigationFieldItems = new DocumentQuery<Navigation>()
                .TopN(GetNavRequest.NumItems)
                .Select(x => new Navigation.NavigationFields(x));

            return new JsonResult(NavigationFieldItems);
        }
    }

    public class GetNavigationItemsRequest
    {
        public int NumItems { get; set; } = 100;
    }
}
