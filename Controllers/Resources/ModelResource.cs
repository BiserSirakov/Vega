namespace Vega.Controllers.Resources
{
    using System.ComponentModel.DataAnnotations;

    public class ModelResource
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
