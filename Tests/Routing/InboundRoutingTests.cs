using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using WebUI;
using System.Web;
using NUnit.Framework;

namespace Tests.Routing
{
    [TestFixture]
    public class InboundRoutingTests
    {
        [Test]
        public void Slash_Goes_To_Home_Dashboard()
        {
            TestRoute("~/", new
            {
                controller = "Home",
                action = "Dashboard"
            });
        }

        [Test]
        public void Testing_Define_Goes_To_Testing_Define()
        {
            TestRoute("~/Testing/Define", new
            {
                controller = "Testing",
                action = "Define"
            });
        }

        private void TestRoute(string url, object expectedValues)
        {
            // Arrange: Prepare the route collection and a mock request context 
            RouteCollection routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            var mockHttpContext = new Moq.Mock<HttpContextBase>();
            var mockRequest = new Moq.Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);
            // Act: Get the mapped route 
            RouteData routeData = routes.GetRouteData(mockHttpContext.Object);
            // Assert: Test the route values against expectations 
            Assert.IsNotNull(routeData);
            var expectedDict = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                else
                    Assert.AreEqual(expectedVal.Value.ToString(),
                    routeData.Values[expectedVal.Key].ToString());
            }
        }
    }
}
