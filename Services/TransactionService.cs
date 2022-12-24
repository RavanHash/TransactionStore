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

    //public async Task<long> Withdraw(TransactionModel transaction)
    //{

    //    return ;
    //}

    //public async Task<List<long>> AddTransfer(List<TransactionModel> transfersModels)
    //{



    //    return ;
    //}

    //public async Task<decimal?> GetBalanceByAccountId(int accountId)
    //{


    //    return ;
    //}


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
