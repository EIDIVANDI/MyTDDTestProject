using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using MyProject;

namespace HaveYouSeenMe.Tests.Routes
{
    [TestClass]
    public class RoutesTest
    {
        RouteCollection routes;
        public RoutesTest()
        {
            // Create the routes table
            routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
        }

        [TestMethod]
        public void DefaultRoute_HomePage_HomeControllerIndexActionOptionalId()
        {
            // Arrange
            Mock<HttpContextBase> mockContextBase = new Mock<HttpContextBase>();

            mockContextBase.Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
            .Returns("~/");

            // Act
            RouteData routeData = routes.GetRouteData(mockContextBase.Object);

            // Assert
            Assert.IsNotNull(routeData);

            Assert.AreEqual("Home", routeData.Values["controller"]);

            Assert.AreEqual("Index", routeData.Values["action"]);

            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void IgnoreRoute_AXDResource_StopRoutingHandler()
        {
            // Arrange
            Mock<HttpContextBase> mockContextBase = new Mock<HttpContextBase>();

            mockContextBase.Setup(x => x.Request.AppRelativeCurrentExecutionFilePath)
            .Returns("~/Resource.axd");

            // Act
            RouteData routeData = routes.GetRouteData(mockContextBase.Object);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.IsInstanceOfType(routeData.RouteHandler, typeof(StopRoutingHandler));
        }


    }
}
