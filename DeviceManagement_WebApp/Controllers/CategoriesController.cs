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

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            //Using .GetAll() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to display all categories
            return View(_categoriesRepository.GetAll());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to find the specific category by ID
            var category = _categoriesRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();

            //Using .Add() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to add the record to the database
            _categoriesRepository.Add(category);
            //Using .Save() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to save the added record to the database
            _categoriesRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to find the specific category by ID
            var category = _categoriesRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            try
            {
                //Using .Update() method from CategoriesRepository (which is inherited from the GenericRepository)
                //to update the existing record
                _categoriesRepository.Update(category);
                //Using .Save() method from CategoriesRepository (which is inherited from the GenericRepository)
                //to save the update made to the existing record
                _categoriesRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CategoryExists(category.CategoryId) == false)
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

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Using .GetById() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to find the specific category by ID
            var category = _categoriesRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoriesRepository.GetById(id);

            //Using .Remove() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to remove an existing record
            _categoriesRepository.Remove(category);
            //Using .Save() method from CategoriesRepository (which is inherited from the GenericRepository)
            //to save removed existing record
            _categoriesRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
            //Check if ID exists then return true if it exists or false if it does not exists
            return _categoriesRepository.Any(id);
        }
    }
}
