namespace Vega.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data.Models;

    public interface IVehiclesService
    {
        QueryResult<Vehicle> GetAll(VehicleQuery filter, bool withDeleted = false);

        Vehicle GetById(int id, bool withIncludings = true);

        void Add(Vehicle vehicle);

        void Delete(Vehicle vehicle, bool hardDelete = false);

        void Save();
    }
}