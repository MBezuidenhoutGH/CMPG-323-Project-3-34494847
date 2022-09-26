using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class DevicesRepository : GenericRepository<Device>, IDevicesRepository
    {
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        public Device GetMostRecentService()
        {
            return _context.Device.OrderByDescending(device => device.DateCreated).FirstOrDefault();
        }

        //USING THE RULE OF INHERITANCE TO MAKE UNIQUE METHODS FOR THIS REPO ONLY
        //BECAUSE OF IT'S UNIQUE LAMBDA FUNCTION REQUIREMENTS THAT IS ONLY NEEDED FOR THE DeviceController AND NOT CategoriesController or ZonesController
        public IEnumerable<Device> GetAllDevices()
        {
            return _context.Device.Include(d => d.Category).Include(d => d.Zone).ToList();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        public IEnumerable<Zone> GetAllZones()
        {
            return _context.Zone.ToList();
        }
    }
}
