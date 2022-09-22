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

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            //Using .GetAll() method from ZonesRepository (which is inherited from the GenericRepository)
            //to display all zones
            return View(_zonesRepository.GetAll());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from ZonesRepository (which is inherited from the GenericRepository)
            //to find the specific zone by ID
            var zone = _zonesRepository.GetById(id);

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();

            //Using .Add() method from ZonesRepository (which is inherited from the GenericRepository)
            //to add the record to the database
            _zonesRepository.Add(zone);
            //Using .Save() method from ZonesRepository (which is inherited from the GenericRepository)
            //to save the added record to the database
            _zonesRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from ZonesRepository (which is inherited from the GenericRepository)
            //to find the specific zone by ID
            var zone = _zonesRepository.GetById(id);

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            try
            {
                //Using .Update() method from ZonesRepository (which is inherited from the GenericRepository)
                //to update the existing record
                _zonesRepository.Update(zone);
                //Using .Save() method from ZonesRepository (which is inherited from the GenericRepository)
                //to save the update made to the existing record
                _zonesRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from ZonesRepository (which is inherited from the GenericRepository)
            //to find the specific zone by ID
            var zone = _zonesRepository.GetById(id);

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zonesRepository.GetById(id);

            //Using .Remove() method from ZonesRepository (which is inherited from the GenericRepository)
            //to remove an existing record
            _zonesRepository.Remove(zone);
            //Using .Save() method from ZonesRepository (which is inherited from the GenericRepository)
            //to save removed existing record
            _zonesRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(Guid id)
        {
            //Using .Any() method from ZonesRepository (which is inherited from the GenericRepository)
            //to return a bool if specific record exists
            return _zonesRepository.CheckID(id);
        }
    }
}
