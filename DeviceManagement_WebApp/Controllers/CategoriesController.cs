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
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // GET: Categories - Display all categories in Index View
        public async Task<IActionResult> Index()
        {    
            return View(_categoriesRepository.GetAll());
        }

        // GET: Categories - Display specific category in Details View by parsing ID
        public async Task<IActionResult> Details(Guid? id)
        {
            if (_categoriesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_categoriesRepository.CheckDetails(id));
        }

        // GET: Categories/Create - No special methods needed
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create - Create a category in Create View by parsing a class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _categoriesRepository.Create(category);
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit - Display specific category in Edit View by parsing ID
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (_categoriesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_categoriesRepository.CheckDetails(id));
        }

        // POST: Categories/Edit - Edit specific category in Edit View by parsing ID and class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
                return NotFound();

            try { 
                _categoriesRepository.Edit(category);
            }
            catch (DbUpdateConcurrencyException){
                if (_categoriesRepository.CheckID(category.CategoryId))
                    throw;
                else
                    return NotFound();       
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete - Display specific category in Delete View by parsing ID
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (_categoriesRepository.CheckDetails(id) == null)
                return NotFound();
            else
                return View(_categoriesRepository.CheckDetails(id));
        }

        // POST: Categories/Delete - Delete specific category in Delete View by parsing ID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _categoriesRepository.DeleteConfirmed(_categoriesRepository.GetById(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
