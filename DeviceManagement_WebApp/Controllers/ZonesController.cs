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
    public class ZonesController : Controller
    {
        private readonly IZonesRepository _zonesRepository;

        public ZonesController(IZonesRepository zonesRepository)
        {
            _zonesRepository = zonesRepository;
        }

        // GET: Zones - Display all zones in Index View
        public async Task<IActionResult> Index()
        {
            return View(_zonesRepository.GetAll());
        }

        // GET: Zones/Details - Display specific zone in Details View by parsing ID
        public async Task<IActionResult> Details(Guid? id)
        {
            if (_zonesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_zonesRepository.CheckDetails(id));
        }

        // GET: Zones/Create - No special methods needed
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create - Create a zone in Create View by parsing a class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _zonesRepository.Create(zone);
            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit - Display specific zone in Edit View by parsing ID
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (_zonesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_zonesRepository.CheckDetails(id));
        }

        // POST: Zones/Edit - Edit specific zone in Edit View by parsing ID and class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
                return NotFound();

            if (_zonesRepository.Edit(id, zone) == false)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Delete - Display specific zone in Delete View by parsing ID
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (_zonesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_zonesRepository.CheckDetails(id));
        }

        // POST: Zones/Delete - Delete specific zone in Delete View by parsing ID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _zonesRepository.DeleteConfirmed(_zonesRepository.GetById(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
