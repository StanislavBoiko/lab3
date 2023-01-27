using Autofac.Extras.Moq;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Moq;

namespace BLL.Tests;

public class CategoryServiceTests
{
    [Fact]
    public void GetAllCategories_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IUnitOfWork>().
                Setup(u => u.CategoryRepo.Get(null)).Returns(GetSampleCategories());
            var cls = mock.Create<CategoryService>();
            var expected = GetSampleCategories();
            var actual = cls.GetAllCategories().ToList();
            
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
                Assert.Equal(expected[i].Name, actual[i].Name);
            }
            
            

        }
    }
    [Fact]
    public void AddCategory_ShouldCallOnlyOnce()
    {
        using (var mock = AutoMock.GetLoose())
        {
            Category category = GetSampleCategories()[0];

            mock.Mock<IUnitOfWork>()
                .Setup(x => x.CategoryRepo.Create(category));
            mock.Mock<IUnitOfWork>().Setup(x => x.Save());

            var cls = mock.Create<CategoryService>();
            cls.AddCategory(category);
            
            mock.Mock<IUnitOfWork>().Verify(x => x.CategoryRepo.Create(category), Times.Exactly(1));
            mock.Mock<IUnitOfWork>().Verify(x => x.Save(), Times.Exactly(1));
            
        }
    }

    [Fact]
    public void GetById_ShouldCallOnce()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IUnitOfWork>()
                .Setup(x => x.CategoryRepo.GetById(1)).Returns(GetSampleCategories()[0]);
            var cls = mock.Create<CategoryService>();
            cls.GetById(1);
            
            mock.Mock<IUnitOfWork>().Verify(x => x.CategoryRepo.GetById(1), Times.Exactly(1));
            
        }
    }

    private List<Category> GetSampleCategories()
    {
        List<Category> output = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Devochki"
            },
            new Category
            {
                Id = 2,
                Name = "Pivo"
            },
            new Category
            {
                Id = 3,
                Name = "Philosophical literature"
            }

        };
        return output;
    }
}