namespace Vega.Controllers
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    using AutoMapper;

    using Data.Models;
    using Resources;
    using Services.Contracts;
    using System.Collections.Generic;

    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : BaseController
    {
        private readonly IPhotosService _photos;
        private readonly IVehiclesService _vehicles;
        private readonly IHostingEnvironment _host;

        private readonly PhotoSettings _photoSettings;

        public PhotosController(IMapper mapper,
            IPhotosService photos,
            IVehiclesService vehicles,
            IHostingEnvironment host,
            IOptionsSnapshot<PhotoSettings> options)
            : base(mapper)
        {
            _photos = photos;
            _vehicles = vehicles;
            _host = host;
            _photoSettings = options.Value;
        }

        [HttpGet]
        public IEnumerable<PhotoResource> GetPhotos(int vehicleId)
        {
            var photos = _photos.GetPhotos(vehicleId);

            return Mapper.Map<IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public IActionResult Upload(int vehicleId, IFormFile file)
        {
            var vehicle = _vehicles.GetById(vehicleId, withIncludings: false);
            if (vehicle == null)
                return NotFound("Vehicle not found.");

            if (file == null)
                return BadRequest("Null file.");
            if (file.Length == 0)
                return BadRequest("Empty file.");
            if (file.Length > _photoSettings.MaxBytes)
                return BadRequest("File bigger than 10 MB.");
            if (!_photoSettings.IsSupported(file.FileName))
                return BadRequest("Invalid file type.");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            _vehicles.Save();

            var result = Mapper.Map<PhotoResource>(photo);

            return Ok(result);
        }
    }
}