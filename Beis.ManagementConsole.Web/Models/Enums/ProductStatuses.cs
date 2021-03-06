namespace Beis.ManagementConsole.Web.Models.Enums
{
    public enum ProductStatuses
    {
        [Display(Name = "Incomplete")]
        Incomplete = 1,
        
        [Display(Name = "In review")]
        InReview = 10,
        
        [Display(Name = "Approved")]
        Approved = 50,
        
        [Display(Name = "Not in scheme")]
        NotInScheme = 1000
    }
}