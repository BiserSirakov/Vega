namespace Vega.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Common.Models;

    public class Make : BaseModel
    {
        public Make()
        {
            this.Models = new HashSet<Model>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
