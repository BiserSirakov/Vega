namespace Vega.Controllers.Resources
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MakeResource
    {
        public MakeResource()
        {
            this.Models = new Collection<ModelResource>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<ModelResource> Models { get; set; }
    }
}
