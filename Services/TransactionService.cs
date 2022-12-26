using LoggerService;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Services;
public class TransactionService : ITransactionService
{
    private readonly ILoggerManager _logger;
    private readonly ITransactionStoredProcedure _transactionStoredProcedure;
    private readonly ICalculationService _calculationService;

    public TransactionService(ILoggerManager logger, ITransactionStoredProcedure transactionStoredProcedure, ICalculationService calculationService)
    {
        _logger = logger;
        _transactionStoredProcedure = transactionStoredProcedure;
        _calculationService = calculationService;
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
        var transfersConvert = await _calculationService.ConvertCurrency(transfersModels);

        _logger.LogInformation("Business layer: Query to data base for add transaction");
        _transactionStoredProcedure.InsertTransaction(transfersConvert[0]);

        _logger.LogInformation("Business layer: Query to data base for add transaction");
        _transactionStoredProcedure.InsertTransaction(transfersConvert[1]);

        var transaction1 = await _transactionStoredProcedure.SelectTransactionById(transfersConvert[0].Id);
        var transaction2 = await _transactionStoredProcedure.SelectTransactionById(transfersConvert[1].Id);

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
