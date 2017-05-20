namespace Vega.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Data.Common;
    using Data.Models;
    using Contracts;

    public class VehiclesService : IVehiclesService
    {
        private readonly IDbRepository<Vehicle> _vehicles;

        public VehiclesService(IDbRepository<Vehicle> vehicles)
        {
            _vehicles = vehicles;
        }

        public IEnumerable<Vehicle> GetAll(Filter filter, bool withDeleted = false)
        {
            var query = _vehicles.GetAll(withDeleted, 
                vehicles => vehicles
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .Include(v => v.Features).ThenInclude(vf => vf.Feature));
                
            if (filter.MakeId.HasValue) 
            {
                query = query.Where(x => x.Model.MakeId == filter.MakeId);   
            }

            return query.ToList();
        }

        public Vehicle GetById(int id, bool withIncludings = true)
        {
            if (!withIncludings)
            {
                return _vehicles.GetById(id);
            }

            return _vehicles.GetById(id, vehicle => vehicle
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .Include(v => v.Features).ThenInclude(vf => vf.Feature)
            );
        }

        public void Add(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
        }

        public void Delete(Vehicle vehicle, bool hardDelete = false)
        {
            _vehicles.Delete(vehicle, hardDelete);
        }

        public void Save()
        {
            _vehicles.Save();
        }
    }
}