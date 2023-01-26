using Autofac.Extras.Moq;
using DataAccessLayer.Repositories;
using Moq;
namespace BLL.Tests;

public class AccountServiceTests
{
    [Fact]
    public void GetAllAccounts_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IUnitOfWork>().Setup(u => u.AccountRepo)
        }
    }
}