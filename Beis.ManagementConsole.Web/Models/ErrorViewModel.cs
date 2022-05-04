namespace Beis.ManagementConsole.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; internal set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}