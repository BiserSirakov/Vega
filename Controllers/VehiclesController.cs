namespace Vega.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;

    using Data;
    using Data.Models;
    using Resources;
    using Services.Contracts;

    [Route("/api/vehicles")]
    public class VehiclesController : BaseController
    {
        private readonly IVehiclesService _vehicles;

        public VehiclesController(IMapper mapper, IVehiclesService vehicles)
            : base(mapper)
        {
            _vehicles = vehicles;
        }

        [HttpGet]
        public QueryResultResource<VehicleResource> GetVehicles(VehicleQueryResource filterResource)
        {
            var filter = Mapper.Map<VehicleQuery>(filterResource);
            var vehicles = _vehicles.GetAll(filter);
            
            return Mapper.Map<QueryResultResource<VehicleResource>>(vehicles);
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            var vehicle = _vehicles.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = Mapper.Map<Vehicle>(saveVehicleResource);

            _vehicles.Add(vehicle);
            _vehicles.Save();

            var result = Mapper.Map<VehicleResource>(_vehicles.GetById(vehicle.Id));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = _vehicles.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            Mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);
            _vehicles.Save();

            var result = Mapper.Map<VehicleResource>(_vehicles.GetById(id));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _vehicles.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _vehicles.Delete(vehicle);
            _vehicles.Save();

            return Ok(id);
        }
    }
}