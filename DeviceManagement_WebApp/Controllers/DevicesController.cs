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
        private readonly IDevicesRepository _devicesRepository;

        public DevicesController(IDevicesRepository devicesRepository)
        {
            _devicesRepository = devicesRepository;
        }

        // GET: Devices - Display all Devices in Index View
        public async Task<IActionResult> Index()
        {

            return View(_devicesRepository.GetAllDevices());
        }

        // GET: Devices/Details - Display specific Device in Details View by parsing ID
        public async Task<IActionResult> Details(Guid? id)
        {
            if (_devicesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_devicesRepository.CheckDetails(id));
        }

        // GET: Devices/Create - No special methods needed
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_devicesRepository.GetAllCategories(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_devicesRepository.GetAllZones(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create - Create a device in Create View by parsing a class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _devicesRepository.Create(device);
            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Edit - Display specific device in Edit View by parsing ID
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (_devicesRepository.CheckDetails(id) == null)
                return NotFound();
            else
            {
                ViewData["CategoryId"] = new SelectList(_devicesRepository.GetAllCategories(), "CategoryId", "CategoryName", _devicesRepository.GetById(id).CategoryId);
                ViewData["ZoneId"] = new SelectList(_devicesRepository.GetAllZones(), "ZoneId", "ZoneName", _devicesRepository.GetById(id).ZoneId);
                return View(_devicesRepository.CheckDetails(id));
            }
        }

        // POST: Devices/Edit - Edit specific device in Edit View by parsing ID and class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
                return NotFound();

            if (_devicesRepository.Edit(id, device) == false)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Delete - Display specific device in Delete View by parsing ID
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (_devicesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_devicesRepository.CheckDetails(id));
        }

        // POST: Devices/Delete - Delete specific device in Delete View by parsing ID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _devicesRepository.DeleteConfirmed(_devicesRepository.GetById(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
