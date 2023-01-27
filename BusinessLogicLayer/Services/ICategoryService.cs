using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services;

public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories();
    void AddCategory(Category category);
    Category GetById(int id);
}