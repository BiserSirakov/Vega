namespace Vega.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data.Common;
    using Data.Models;
    using Extensions;

    public class VehiclesService : IVehiclesService
    {
        private readonly IDbRepository<Vehicle> _vehicles;

        public VehiclesService(IDbRepository<Vehicle> vehicles)
        {
            _vehicles = vehicles;
        }

        public QueryResult<Vehicle> GetAll(VehicleQuery filter, bool withDeleted = false)
        {
            var result = new QueryResult<Vehicle>();

            var query = _vehicles.GetAll(vehicles => vehicles
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .Include(v => v.Features).ThenInclude(vf => vf.Feature), withDeleted);

            if (filter.MakeId.HasValue)
            {
                query = query.Where(x => x.Model.MakeId == filter.MakeId.Value);
            }

            if (filter.ModelId.HasValue)
            {
                query = query.Where(x => x.ModelId == filter.ModelId.Value);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };

            query = query.ApplyOrdering(filter, columnsMap);

            result.TotalItems = query.Count();

            query = query.ApplyPaging(filter);

            result.Items = query.ToList();

            return result;
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