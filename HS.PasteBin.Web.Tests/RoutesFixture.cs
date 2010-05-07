using System.Reflection;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;

namespace HS.PasteBin.Web.Tests
{
    class MockRequest : HttpRequestBase
    {
        private readonly string appRelativeUrl;

        public MockRequest(string appRelativeUrl)
        {
            this.appRelativeUrl = appRelativeUrl;
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return appRelativeUrl; }
        }

        public override string PathInfo
        {
            get { return string.Empty; }
        }
    }

    class MockContext : HttpContextBase
    {
        private readonly HttpRequestBase request;

        public MockContext(HttpRequestBase request)
        {
            this.request = request;
        }

        public override HttpRequestBase Request
        {
            get { return request; }
        }
    }

    [TestFixture]
    public class RoutesFixture
    {
        private static void AssertRoutesMatch(object values, RouteData routeData)
        {
            foreach (var property in values.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Assert.IsTrue(routeData.Values.ContainsKey(property.Name));
                Assert.AreEqual(property.GetValue(values, null), routeData.Values[property.Name]);
            }
        }

        [Test]
        public void DefaultUrlRoutesToCreateAction()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            AssertRoutesMatch
                (
                new { controller = "Paste", action = "Create" },
                routes.GetRouteData(new MockContext(new MockRequest("~/")))
                );
        }


        [Test]
        public void InstallRoutesToAdminInstallAction()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            AssertRoutesMatch
                (
                new { controller = "Admin", action = "Install" },
                routes.GetRouteData(new MockContext(new MockRequest("~/Install")))
                );
        }

        [Test]
        public void CreateRoutesToPasteCreateAction()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            AssertRoutesMatch
                (
                new { controller = "Paste", action = "Create" },
                routes.GetRouteData(new MockContext(new MockRequest("~/Create")))
                );
        }

        [Test]
        public void RecentRoutesToPasteRecentAction()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            AssertRoutesMatch
                (
                new { controller = "Paste", action = "Recent" },
                routes.GetRouteData(new MockContext(new MockRequest("~/Recent")))
                );
        }

        [Test]
        public void SaveRoutesToPasteSaveAction()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            AssertRoutesMatch
                (
                new { controller = "Paste", action = "Save" },
                routes.GetRouteData(new MockContext(new MockRequest("~/Save")))
                );
        }

        [Test]
        public void ShowRoutesToPasteShowActionWithCorrectID()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            AssertRoutesMatch
                (
                new { controller = "Paste", action = "Show", id = "42" },
                routes.GetRouteData(new MockContext(new MockRequest("~/42")))
                );
        }
    }
}
