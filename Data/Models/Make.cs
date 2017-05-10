namespace Vega.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Make
    {
        public Make()
        {
            this.Models = new HashSet<Model>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
