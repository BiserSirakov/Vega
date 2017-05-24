namespace Vega.Services.Contracts
{
    using System.Collections.Generic;
    using Data.Models;

    public interface IPhotosService
    {
        IEnumerable<Photo> GetPhotos(int vehicleId);
    }
}