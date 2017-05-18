namespace Vega.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Feature : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
