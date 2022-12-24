using LoggerService;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Services;
public class TransactionService : ITransactionService
{
    private readonly ILoggerManager _logger;
    private readonly ITransactionStoredProcedure _transactionStoredProcedure;

    public TransactionService(ILoggerManager logger, ITransactionStoredProcedure transactionStoredProcedure)
    {
        _logger = logger;
        _transactionStoredProcedure = transactionStoredProcedure;
    }

    public async Task<TransactionModel> AddDeposit(TransactionModel transaction, int transactionId)
    {
        _logger.LogInformation("Business layer: Query to data base for add transaction");
        _transactionStoredProcedure.InsertTransaction(transaction);

        _logger.LogInformation("Business layer: Query to data base to return transaction");
        return await _transactionStoredProcedure.SelectTransactionById(transactionId);
    }

    public async Task<TransactionModel> Withdraw(TransactionModel transaction, int transactionId)
    {
        _logger.LogInformation("Business layer: Query to data base for add transaction");
        _transactionStoredProcedure.InsertTransaction(transaction);

        _logger.LogInformation("Business layer: Query to data base to return transaction");
        return await _transactionStoredProcedure.SelectTransactionById(transactionId);
    }

    public async Task<List<TransactionModel>> AddTransfer(List<TransactionModel> transfersModels)
    {
        _logger.LogInformation("Business layer: Query to data base for add transaction");
        _transactionStoredProcedure.InsertTransaction(transfersModels[0]);

        _logger.LogInformation("Business layer: Query to data base for add transaction");
        _transactionStoredProcedure.InsertTransaction(transfersModels[1]);

        var transaction1 = await _transactionStoredProcedure.SelectTransactionById(transfersModels[0].Id);
        var transaction2 = await _transactionStoredProcedure.SelectTransactionById(transfersModels[1].Id);

        _logger.LogInformation("Business layer: Query to data base to return transactions");
        return new List<TransactionModel>
        {
            transaction1,
            transaction2
        };
    }

    public async Task<decimal?> GetBalanceByUserId(int userId)
    {
        _logger.LogInformation($"Business layer: Balance by account id {userId} returned to controller");
        return await _transactionStoredProcedure.GetBalanceByUserId(userId);
    }


    public async Task<IEnumerable<TransactionModel>> GetAllTransactions()
    {
        _logger.LogInformation("Business layer: Query in data base for transactions receiving");
        _logger.LogInformation("Business layer: All Transactions returned to controller");
        return await _transactionStoredProcedure.SelectAllTransactions();
    }

    public async Task<TransactionModel> GetTransactionById(int id)
    {
        _logger.LogInformation("Business layer: Query in data base for transaction receiving");
        _logger.LogInformation("Business layer: Transaction returned to controller");
        return await _transactionStoredProcedure.SelectTransactionById(id);
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByUserId(int userId)
    {
        _logger.LogInformation($"Business layer: Query in database for transaction by accountId {userId}");
        _logger.LogInformation("Business layer: Transactions returned to controller");
        return await _transactionStoredProcedure.SelectAllTransactionsByUserId(userId);
    }
}
