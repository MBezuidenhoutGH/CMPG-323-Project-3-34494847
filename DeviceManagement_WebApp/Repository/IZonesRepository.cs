using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repository
{
    //Interface for Zones that is inheriting the method definitions in IGenericRepository to use in the ZonesController
    public interface IZonesRepository : IGenericRepository<Zone>
    {
        Zone GetMostRecentService();
    }
}
