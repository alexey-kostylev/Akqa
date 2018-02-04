using Akqa.Logic;
using Akqa.Web.Controllers;
using Akqa.Web.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Akqa.Tests
{
    [TestClass]
    public class ControllerTests : TestBase
    {
        private readonly Mock<INumberConverter> _mockConverter = new Mock<INumberConverter>();

        private HomeController _controller;

        [TestInitialize]
        public void Init()
        {
            _controller = new HomeController(_mockConverter.Object);
        }

        [TestMethod]
        public void NullConverterShouldThrowException()
        {
            Action act = () => new HomeController(null);

            act.ShouldThrow<ArgumentNullException>();            
        }

        [TestMethod]
        public void ShouldGetIndexView()
        {
            var view = _controller.Index();

            _mockConverter.Verify(x => x.Convert(It.IsAny<decimal>()), Times.Never);

            view.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldDoPostAndRedirect()
        {
            var tempDataDictionary = _controller.TempData;

            var model =  new ConvertingModel
            {
                Number = 111,
                Name = "test"
            };

            _mockConverter.Setup(x => x.Convert(111))
                .ReturnsAsync("some number");

            var result = await _controller.Index(model);

            result.Should().BeOfType<RedirectToRouteResult>();

            _mockConverter.Verify(x => x.Convert(111), Times.Once);

            var redirect = result as RedirectToRouteResult;
            redirect.RouteValues["action"].Should().Be("Converted");            

            var tempModel = tempDataDictionary["model"] as ConvertedModel;

            tempModel.Should().NotBeNull();

            tempModel.Name.Should().Be("test");
            tempModel.Text.Should().Be("some number");
        }

        [TestMethod]
        public void ShouldGetConvertedView()
        {
            var model = new ConvertedModel
            {
                Name = "test",
                Text = "text"
            };

            _controller.TempData["model"] = model;
            
            var result = _controller.Converted();

            _mockConverter.Verify(x => x.Convert(It.IsAny<decimal>()), Times.Never);

            result.Should().BeOfType<ViewResult>();

            var viewResult = result as ViewResult;
            viewResult.Model.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<ConvertedModel> ();

            var dataModel = viewResult.Model as ConvertedModel;
            dataModel.Name.Should().Be("test");
            dataModel.Text.Should().Be("text");
        }

        [TestMethod]
        public void ConvertedNullModelPassedShouldRedirectToDefaultView()
        {           

            var result = _controller.Converted();

            _mockConverter.Verify(x => x.Convert(It.IsAny<decimal>()), Times.Never);

            result.Should().BeOfType<RedirectToRouteResult>();

            var redirectResult = result as RedirectToRouteResult;

            redirectResult.RouteValues["action"].Should().Be("Index");            
        }
    }
}
