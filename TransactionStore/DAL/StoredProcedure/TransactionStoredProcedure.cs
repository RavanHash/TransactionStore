using Dapper;
using LoggerService;
using System.Data.SqlClient;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.DAL.StoredProcedure;
public class TransactionStoredProcedure : ITransactionStoredProcedure
{
    private readonly ILoggerManager _logger;
    private readonly IConfiguration _config;

    public TransactionStoredProcedure(ILoggerManager logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public async Task<int> InsertTransaction(TransactionModel transaction)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync("insert into Transactions (UserId, ReceiverId, TransactionAmount, TransactionType, Currency, TransactionDate)" +
           " values (@UserId, @ReceiverId, @TransactionAmount, @TransactionType, @Currency, @TransactionDate)", transaction);

        var id = await connection.QueryAsync<int>("select max(id) from Transactions");

        return id.FirstOrDefault();
    }

    public async Task<IEnumerable<TransactionModel>> SelectAllTransactions()
    {
        _logger.LogInformation("Data layer: Connection to data base");
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        _logger.LogInformation($"Data layer: All transactions returned to business");
        return await connection.QueryAsync<TransactionModel>("select * from Transactions");
    }
    public async Task<TransactionModel> SelectTransactionById(int transactionId)
    {
        _logger.LogInformation("Data layer: Connection to data base");
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        _logger.LogInformation($"Data layer: Transaction id {transactionId} returned to business");
        return await connection.QueryFirstAsync<TransactionModel>("select * from Transactions where id = @Id",
                new { Id = transactionId });
    }

    public async Task<IEnumerable<TransactionModel>> SelectAllTransactionsByUserId(int userId)
    {
        _logger.LogInformation("Data layer: Connection to data base");
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        _logger.LogInformation($"Data layer: Transactions by user id {userId} returned to business");
        return await connection.QueryAsync<TransactionModel>
            ("select * from Transactions where UserId = @Id", new { Id = userId });
    }

    public async Task<decimal?> GetBalanceByUserId(int userId)
    {
        _logger.LogInformation("Data layer: Connection to data base");
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        var balance = await connection.ExecuteScalarAsync<decimal?>
            ("select coalesce(sum([TransactionAmount]),0) from Transactions where UserId = @Id", new { Id = userId });

        _logger.LogInformation($"Data layer: Balance {balance} returned to business");
        return balance;
    }
}
