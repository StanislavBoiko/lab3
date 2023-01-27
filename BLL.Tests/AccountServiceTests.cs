using Autofac.Extras.Moq;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
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
            mock.Mock<IUnitOfWork>().
                Setup(u => u.AccountRepo.Get(null)).Returns(GetSampleAccounts());
            var cls = mock.Create<AccountService>();
            var expected = GetSampleAccounts();
            var actual = cls.GetAllAccounts().ToList();
            
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
    public void CreateAccount_ShouldBeCalledOnlyOnce()
    {
        using (var mock = AutoMock.GetLoose())
        {
            Account account = GetSampleAccounts()[0];

            mock.Mock<IUnitOfWork>()
                .Setup(x => x.AccountRepo.Create(account));
            mock.Mock<IUnitOfWork>().Setup(x => x.Save());

            var cls = mock.Create<AccountService>();
            cls.AddAccount(account);
            
            mock.Mock<IUnitOfWork>().Verify(x => x.AccountRepo.Create(account), Times.Exactly(1));
            mock.Mock<IUnitOfWork>().Verify(x => x.Save(), Times.Exactly(1));
            
        }

    }

    [Fact]
    public void GetAccountById_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IUnitOfWork>()
                .Setup(x => x.AccountRepo.GetById(1)).Returns(GetSampleAccounts()[0]);
            var cls = mock.Create<AccountService>();
            cls.GetAccountById(1);
            
            mock.Mock<IUnitOfWork>().Verify(x => x.AccountRepo.GetById(1), Times.Exactly(1));
            
        }
    }

    [Fact]
    public void GetOtherAccounts_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            Account account = GetSampleAccounts()[1];
            mock.Mock<IUnitOfWork>()
                .Setup(x => x.AccountRepo.Get(a => !a.Equals(account)));

            var cls = mock.Create<AccountService>();
            cls.getOtherAccounts(account);
            
            mock.Mock<IUnitOfWork>()
                .Verify(x => 
                    x.AccountRepo.Get(a => !a.Equals(account)), Times.Exactly(1));
            
            
        }
    } 

    private List<Account> GetSampleAccounts()
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