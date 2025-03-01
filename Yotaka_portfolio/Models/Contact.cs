using System.ComponentModel.DataAnnotations;


namespace Yotaka_portfolio.Models
{
    public class Contact
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Missing top level domain")]
        public string? Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Message cannot exceed 100 characters")]
        public string? Message { get; set; }
    }
}