namespace Vega.Controllers.Resources
{
    using System.ComponentModel.DataAnnotations;

    public class ContactResource
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
    }
}