using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Automation.API.Accelerators.Constants;
using Automation.API.Accelerators.Pages;
using Automation.API.Accelerators.UtilityClasses;

namespace Automation.API.Tests.Steps
{

    [Binding]
    public class GetPostsSteps
    {

        public RestClient client = new RestClient(Constants.URI);
        public RestRequest request = new RestRequest();
        public IRestResponse<Posts> response = new RestResponse<Posts>();


        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {           
            request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [When(@"I perform operation for post ""(.*)""")]
        public void GivenIPerformOperationForPost(int postId)
        {
            var requestPost = Constants.POST + postId.ToString();
            request.AddUrlSegment("postid", requestPost);
            response = client.ExecuteAsyncRequest<Posts>(request).GetAwaiter().GetResult();
        }

        [Then(@"I should see the '(.*)' name as '(.*)'")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            Assert.That(response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matching");
        }

    }
}
