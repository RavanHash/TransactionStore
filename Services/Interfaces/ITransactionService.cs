using TransactionStore.Models;

namespace TransactionStore.Services.Interfaces;
public interface ITransactionService
{
    public Task<TransactionModel> AddDeposit(TransactionModel transaction, int transactionId);

    public Task<IEnumerable<TransactionModel>> GetAllTransactions();

    public Task<TransactionModel> GetTransactionById(int id);

    public Task<IEnumerable<TransactionModel>> GetTransactionsByUserId(int userId);
}
