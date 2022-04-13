namespace Beis.HelpToGrow.Console.Web.UnitTests
{
    using Beis.HelpToGrow.Console.Web.Controllers;
    using Beis.HelpToGrow.Console.Web.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class ErrorControllerTests
    {
        private readonly ErrorController _sut;

        public ErrorControllerTests()
        {
            _sut = new ErrorController();
        }

        [Fact]
        public void ErrorShouldRedirectToViewWithData()
        {
            // Arrange
            _sut.ControllerContext.HttpContext = new DefaultHttpContext { TraceIdentifier = "TraceIdentifier" };

            // Act
            var result = _sut.Error() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as ErrorViewModel;
            Assert.NotNull(model);
            Assert.True(!string.IsNullOrWhiteSpace(model.RequestId));
            Assert.True(model.ShowRequestId);
        }
    }
}