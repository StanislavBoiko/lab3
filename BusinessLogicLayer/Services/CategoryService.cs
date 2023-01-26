using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class CategoryService : ICategoryService
{

    internal IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public IEnumerable<Category> GetAllCategories()
    {
        return _uow.CategoryRepo.Get();
    }

    public void AddCategory(Category category)
    {
        _uow.CategoryRepo.Create(category);
        _uow.Save();
    }

    public void UpdateCategory(Category category)
    {
        _uow.CategoryRepo.Update(category);
        _uow.Save();
    }

    public Category GetById(int id)
    {
        return _uow.CategoryRepo.GetById(id);
    }
}