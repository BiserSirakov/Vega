namespace Vega.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Common.Models;

    public class Model : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }
    }
}
