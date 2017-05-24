namespace Vega.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Data.Common;
    using Data.Models;

    public class PhotosService : IPhotosService
    {
        private readonly IDbRepository<Photo> _photos;

        public PhotosService(IDbRepository<Photo> photos)
        {
            _photos = photos;
        }

        public IEnumerable<Photo> GetPhotos(int vehicleId)
        {
            return _photos.GetAll(x => x.Where(v => v.VehicleId == vehicleId)).ToList();
        }
    }
}