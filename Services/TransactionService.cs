using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Services;
public class TransactionService : ITransactionService
{
    private readonly ITransactionStoredProcedure _transactionStoredProcedure;

    public TransactionService(ITransactionStoredProcedure transactionStoredProcedure)
    {
        _transactionStoredProcedure = transactionStoredProcedure;
    }

    public async Task<TransactionModel> AddDeposit(TransactionModel transaction, int transactionId)
    {
        _transactionStoredProcedure.InsertTransaction(transaction);

        return await _transactionStoredProcedure.SelectTransactionById(transactionId);
    }

    public async Task<TransactionModel> Withdraw(TransactionModel transaction, int transactionId)
    {
        _transactionStoredProcedure.InsertTransaction(transaction);

        return await _transactionStoredProcedure.SelectTransactionById(transactionId);
    }

    public async Task<List<TransactionModel>> AddTransfer(List<TransactionModel> transfersModels)
    {
        _transactionStoredProcedure.InsertTransaction(transfersModels[0]);
        _transactionStoredProcedure.InsertTransaction(transfersModels[1]);

        var transaction1 = await _transactionStoredProcedure.SelectTransactionById(transfersModels[0].Id);
        var transaction2 = await _transactionStoredProcedure.SelectTransactionById(transfersModels[1].Id);

        return new List<TransactionModel>
        {
            transaction1,
            transaction2
        };
    }

    public async Task<decimal?> GetBalanceByUserId(int userId)
    {
        return await _transactionStoredProcedure.GetBalanceByUserId(userId);
    }


    public async Task<IEnumerable<TransactionModel>> GetAllTransactions()
    {
        return await _transactionStoredProcedure.SelectAllTransactions();
    }

    public async Task<TransactionModel> GetTransactionById(int id)
    {
        return await _transactionStoredProcedure.SelectTransactionById(id);
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByUserId(int userId)
    {
        return await _transactionStoredProcedure.SelectAllTransactionsByUserId(userId);
    }
}
