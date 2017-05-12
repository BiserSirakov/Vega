namespace Vega.Controllers.Resources
{
    using System.ComponentModel.DataAnnotations;

    public class FeatureResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
