namespace Vega.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Data.Models;

    public interface IVehiclesService
    {
        IEnumerable<Vehicle> GetAll(bool withDeleted = false);

        Vehicle GetById(int id, bool withIncludings = true);

        void Add(Vehicle vehicle);

        void Delete(Vehicle vehicle, bool hardDelete = false);

        void Save();
    }
}