namespace Vega.Controllers.Resources
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    public class MakeResource : KeyValuePairResource
    {
        public MakeResource()
        {
            this.Models = new Collection<KeyValuePairResource>();
        }

        public ICollection<KeyValuePairResource> Models { get; set; }
    }
}
