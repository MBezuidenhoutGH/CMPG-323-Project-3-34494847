using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repository
{
    //Interface for Categories that is inheriting the method definitions in IGenericRepository to use in the CategoriesController
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Category GetMostRecentService();
    }
}
