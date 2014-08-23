using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Routing;
using WebUI;
using System.Web;

namespace Tests.Routing
{
    [TestFixture]
    public class OutboundRoutingTests
    {
        [Test]
        public void Home_Dashboard_Is_At_Slash()
        {
            Assert.AreEqual("/", GetOutboundUrl(new
            {
                controller = "Home",
                action = "Dashboard"
            }));
        }

        [Test]
        public void Testing_Define_Is_At_Slash_Testing_Define()
        {
            Assert.AreEqual("/Testing/Define", GetOutboundUrl(new
            {
                controller = "Testing",
                action = "Define"
            }));
        }

        [Test]
        public void Anything_Controller_Else_Action_Is_At_Anything_Slash_Else()
        {
            Assert.AreEqual("/Anything/Else", GetOutboundUrl(new
            {
                controller = "Anything",
                action = "Else"
            }));
        }
        
        string GetOutboundUrl(object routeValues)
        {
            //Get route configuration and mock request context 
            RouteCollection routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
            var mockHttpContext = new Moq.Mock<HttpContextBase>();
            var mockRequest = new Moq.Mock<HttpRequestBase>();
            var fakeResponse = new FakeResponse();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockHttpContext.Setup(x => x.Response).Returns(fakeResponse);
            mockRequest.Setup(x => x.ApplicationPath).Returns("/");
            // Generate the outbound URL 
            var ctx = new RequestContext(mockHttpContext.Object, new RouteData());
            return routes.GetVirtualPath(ctx, new RouteValueDictionary(routeValues))
            .VirtualPath;
        }

        private class FakeResponse : HttpResponseBase
        {
            // Routing calls this to account for cookieless sessions 
            //It'sirrelevantforthetest,sojust return the path unmodified 
            public override string ApplyAppPathModifier(string x) { return x; }
        }
    }

}
