namespace Beis.HelpToGrow.Console.Web.UnitTests
{
    using System.Collections.Generic;

    using Beis.HelpToGrow.Console.Web.Attributes;
    using Beis.HelpToGrow.Console.Web.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Xunit;

    public class HttpResponseExceptionAttributeTests
    {
        private readonly HttpResponseExceptionAttribute _sut;

        public HttpResponseExceptionAttributeTests()
        {
            _sut = new HttpResponseExceptionAttribute();
        }

        [Fact]
        public void ShouldGetTheFilterOrder()
        {
            // Arrange

            // Act

            //Assert
            Assert.True(_sut.Order == int.MaxValue - 10);
        }

        [Fact]
        public void OnActionExecutedShouldCheckForHttpResponseException()
        {
            // Arrange
            var context = new ActionExecutedContext(
                new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
                new List<IFilterMetadata>(),
                default!)
            {
                Exception = new HttpResponseException
                {
                    Value = "value",
                    Status = 500
                }
            };

            // Act
            _sut.OnActionExecuted(context);

            //Assert
            Assert.True(context.ExceptionHandled);
            var result = context.Result as ObjectResult;
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 500);
        }
    }
}