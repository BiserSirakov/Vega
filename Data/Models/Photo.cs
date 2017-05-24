namespace Vega.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Photo : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string FileName { get; set; }

        public int VehicleId { get; set; }
    }
}