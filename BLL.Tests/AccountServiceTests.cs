using Autofac.Extras.Moq;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Moq;
namespace BLL.Tests;

public class AccountServiceTests
{
    private Mock<IRepository<Account>> _mockRepo;
    private IAccountService _service;
    private Mock<IUnitOfWork> _mockUow;

    public AccountServiceTests()
    {
        _mockRepo = new Mock<IRepository<Account>>();
        _mockUow = new Mock<IUnitOfWork>();
        _service = new AccountService(_mockUow.Object);
    }
    
    [Fact]
    public void GetAllAccounts_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IUnitOfWork>().Setup(u => u.AccountRepo.Get(null)).Returns(GetSampleAccounts());
            var cls = mock.Create<AccountService>();
            var expected = GetSampleAccounts();
            var actual = cls.GetAllAccounts();
            
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                ;
            }
            
            

        }
    }

    private IEnumerable<Account> GetSampleAccounts()
    {
        List<Account> output = new List<Account>()
        {
            new Account
            {
                Id = 1,
                Name = "Account1"
            },
            new Account
            {
                Id = 2,
                Name = "Account2"
            },
            new Account
            {
                Id = 3,
                Name = "Account3"
            }
        };
        return output;
    }
}