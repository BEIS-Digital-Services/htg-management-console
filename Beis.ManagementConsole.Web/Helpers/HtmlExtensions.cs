using Microsoft.AspNetCore.Html;
using System.Text;
using System.Web;

namespace Beis.ManagementConsole.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static IHtmlContent Nl2Br(this IHtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new HtmlString(text);

            var builder = new StringBuilder();
            var lines = text.Split("\\n");
            for (var i = 0; i < lines.Length; i++)
            {
                if (i > 0)
                    builder.Append("<br/>");
                builder.Append(HttpUtility.HtmlEncode(lines[i]));
            }
            return new HtmlString(builder.ToString());
        }
    }
}