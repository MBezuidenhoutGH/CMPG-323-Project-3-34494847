using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;

namespace DeviceManagement_WebApp.Controllers
{
    public class DevicesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly IDevicesRepository _devicesRepository;

        public DevicesController(ConnectedOfficeContext context, IDevicesRepository devicesRepository)
        {
            _context = context;
            _devicesRepository = devicesRepository;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            //Using .GetAll() method from DevicesRepository (which is inherited from the GenericRepository)
            //to display all devices
            return View(_devicesRepository.GetAll());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from DevicesRepository (which is inherited from the GenericRepository)
            //to find the specific device by ID
            var device = _devicesRepository.GetById(id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();

            //Using .Add() method from DevicesRepository (which is inherited from the GenericRepository)
            //to add the record to the database
            _devicesRepository.Add(device);
            //Using .Save() method from DevicesRepository (which is inherited from the GenericRepository)
            //to save the added record to the database
            _devicesRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from DevicesRepository (which is inherited from the GenericRepository)
            //to find the specific device by ID
            var device = _devicesRepository.GetById(id);

            if (device == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_context.Zone, "ZoneId", "ZoneName", device.ZoneId);

            return View(device);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            try
            {
                //Using .Update() method from DevicesRepository (which is inherited from the GenericRepository)
                //to update the existing record
                _devicesRepository.Update(device);
                //Using .Save() method from DevicesRepository (which is inherited from the GenericRepository)
                //to save the update made to the existing record
                _devicesRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from DevicesRepository (which is inherited from the GenericRepository)
            //to find the specific device by ID
            var device = _devicesRepository.GetById(id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = _devicesRepository.GetById(id);

            //Using .Remove() method from DevicesRepository (which is inherited from the GenericRepository)
            //to remove an existing record
            _devicesRepository.Remove(device);
            //Using .Save() method from DevicesRepository (which is inherited from the GenericRepository)
            //to save removed existing record
            _devicesRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            //Using .Any() method from DevicesRepository (which is inherited from the GenericRepository)
            //to return a bool if specific record exists
            return _devicesRepository.Any(id);
        }
    }
}
