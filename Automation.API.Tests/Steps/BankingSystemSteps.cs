using Automation.API.Accelerators.Constants;
using Automation.API.Accelerators.Pages;
using Automation.API.Accelerators.UtilityClasses;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using TechTalk.SpecFlow;

namespace Automation.API.Tests.Steps
{
    [Binding]
    public class BankingSystemSteps
    {
        public RestClient client = new RestClient(Constants.URI);
        public RestRequest request = new RestRequest();
        public IRestResponse<BankingSystemsPage> response = new RestResponse<BankingSystemsPage>();


        #region

        [Given(@"I hit banking operation systems")]
        public void GivenIHitBankingOperationSystems()
        {
            request = new RestRequest(Constants.URI, RestSharp.Method.GET);
            IRestResponse response = client.Execute(request);            
        }

        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {
            request = new RestRequest(url, RestSharp.Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Given(@"I enter all the mandatory details of user")]
        public void GivenIEnterAllTheMandatoryDetailsOfUser()
        {
            request = new RestRequest(Constants.URI, RestSharp.Method.POST);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        #endregion

        [When(@"I perform operation for post ""(.*)""")]
        public void GivenIPerformOperationForPost(int postId)
        {
            var requestPost = Constants.POST + postId.ToString();
            request.AddUrlSegment("postid", requestPost);
            response = (IRestResponse<BankingSystemsPage>)client.ExecuteAsyncRequest<BankingSystemsPage>(request).GetAwaiter().GetResult();         
        }

        #region

        [Then(@"I should get the response code as '(.*)'")]
        public void ThenIShouldGetTheResponseCodeAs(string statusCode)
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I should see the '(.*)' name as '(.*)'")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            Assert.That(response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matching");
        }

        [Then(@"I should see the user is successfully created")]
        public void ThenIShouldSeeTheUserIsSuccessfullyCreated()
        {
            throw new PendingStepException();
        }


        #endregion

    }
}
