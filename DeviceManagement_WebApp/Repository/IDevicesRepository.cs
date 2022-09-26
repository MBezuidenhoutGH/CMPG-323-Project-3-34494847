using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    //Interface for Devices that is inheriting the method definitions in IGenericRepository to use in the DevicesController
    public interface IDevicesRepository : IGenericRepository<Device>
    {
        Device GetMostRecentService();
        IEnumerable<Device> GetAllDevices();
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Zone> GetAllZones();
    }
}
