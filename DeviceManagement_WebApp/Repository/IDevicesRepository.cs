using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repository
{
    //Interface for Devices that is inheriting the method definitions in IGenericRepository to use in the DevicesController
    public interface IDevicesRepository : IGenericRepository<Device>
    {
        Device GetMostRecentService();
    }
}
