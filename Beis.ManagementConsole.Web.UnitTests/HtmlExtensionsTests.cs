using Beis.ManagementConsole.Web.Helpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Extensions.WebEncoders.Testing;
using Moq;
using Xunit;

namespace Beis.ManagementConsole.Web.UnitTests
{
    public class HtmlExtensionsTests
    {
        private readonly IHtmlHelper _sut;

        public HtmlExtensionsTests()
        {
            _sut = new HtmlHelper(
                new Mock<IHtmlGenerator>().Object,
                new Mock<ICompositeViewEngine>().Object,
                new Mock<IModelMetadataProvider>().Object,
                new Mock<IViewBufferScope>().Object,
                new HtmlTestEncoder(),
                new UrlTestEncoder());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Nl2BrShouldReturnEmptyHtmlStringWhenInputIsInvalid(string input)
        {
            // Arrange

            // Act
            var result = _sut.Nl2Br(input);

            // Assert
            Assert.NotNull(result);
            var htmlString = ((HtmlString)result).Value;
            Assert.True(input == htmlString);
        }

        [Fact]
        public void Nl2BrShouldReturnHtmlString()
        {
            // Arrange
            var input = $"This is a product\\nand name of the product is test product1";

            // Act
            var result = _sut.Nl2Br(input);

            // Assert
            Assert.NotNull(result);
            var htmlString = ((HtmlString)result).Value;
            Assert.NotNull(htmlString);
        }
    }
}