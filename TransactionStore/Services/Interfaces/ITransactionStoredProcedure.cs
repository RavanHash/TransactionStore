using TransactionStore.Models;

namespace TransactionStore.Services.Interfaces;
public interface ITransactionStoredProcedure
{
    public Task<int> InsertTransaction(TransactionModel transaction);

    public Task<IEnumerable<TransactionModel>> SelectAllTransactions();

    public Task<TransactionModel> SelectTransactionById(int transactionId);

    public Task<IEnumerable<TransactionModel>> SelectAllTransactionsByUserId(int userId);

    public Task<decimal?> GetBalanceByUserId(int userId);
}
