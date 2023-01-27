using Autofac.Extras.Moq;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Moq;

namespace BLL.Tests;

public class TransactionServiceTests
{
    [Fact]
    public void CreateTransaction_ShouldCallOnce()
    {
        using (var mock = AutoMock.GetLoose())
        {
            Transaction transaction = GetSampleTransactions()[0];

            mock.Mock<IUnitOfWork>()
                .Setup(x => x.TransactionRepo.Create(transaction));
            mock.Mock<IUnitOfWork>().Setup(x => x.Save());

            var cls = mock.Create<TransactionService>();
            cls.CreateTransaction(transaction);
            
            mock.Mock<IUnitOfWork>().Verify(x => x.TransactionRepo.Create(transaction), Times.Exactly(1));
            mock.Mock<IUnitOfWork>().Verify(x => x.Save(), Times.Exactly(1));
            
        }
    }

    [Fact]
    public void GetTransactionById_ShouldCallOnce()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<IUnitOfWork>()
                .Setup(x => x.TransactionRepo.GetById(1)).Returns(GetSampleTransactions()[0]);
            var cls = mock.Create<TransactionService>();
            cls.GetTransactionById(1);
            
            mock.Mock<IUnitOfWork>().Verify(x => x.TransactionRepo.GetById(1), Times.Exactly(1));
            
        }
    }

    private List<Transaction> GetSampleTransactions()
    {
        List<Transaction> output = new List<Transaction>
        {
            new Transaction
            {
                Id = 1,
                Amount = 228
            },
            new Transaction
            {
                Id = 2,
                Amount = 1337
            },
            new Transaction
            {
                Id = 3,
                Amount = 322
            },
        };
        return output;
    }
}