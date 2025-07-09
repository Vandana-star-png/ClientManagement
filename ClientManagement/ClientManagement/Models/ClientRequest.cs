using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Models
{
    public class ClientRequest
    {
        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(100, ErrorMessage = "Client Name can't exceed 100 characters")]
        public string? ClientName { get; set; }

        [Required(ErrorMessage = "Licence Start date is required")]
        public DateOnly LicenceStartDate { get; set; }

        [Required(ErrorMessage = "Licence End date is required")]
        public DateOnly LicenceEndDate { get; set; }

        [StringLength(250, ErrorMessage = "Description can't exceed 250 characters")]
        public string? Description { get; set; }
    }
}
