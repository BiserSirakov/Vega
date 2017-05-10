namespace Vega.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Feature
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
