using Akqa.Web;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;
using System.Web.Routing;

namespace Akqa.Tests
{
    [TestClass]
    public class MvcCoverageTests : TestBase
    {

        [TestMethod]
        public void ShouldDoUnityActivator()
        {
            UnityMvcActivator.Start();
            UnityMvcActivator.Shutdown();
        }

        [TestMethod]
        public void UnityConfigTest()
        {
            var container = UnityConfig.Container;
            container.Should().NotBeNull();            
        }

        [TestMethod]
        public void RouteConfigTest()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);            
        }

        [TestMethod]
        public void MvcApplicationTest()
        {
            var app = new MvcApplication();
        }

        [TestMethod]
        public void FilterConfigTest()
        {
            FilterConfig.RegisterGlobalFilters(new System.Web.Mvc.GlobalFilterCollection());
        }

        [TestMethod]
        public void BundleConfigTest()
        {
            BundleConfig.RegisterBundles(new BundleCollection());
        }
    }
}
