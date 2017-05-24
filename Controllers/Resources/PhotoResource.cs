namespace Vega.Controllers.Resources
{
    using System;

    public class PhotoResource
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string FileName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
