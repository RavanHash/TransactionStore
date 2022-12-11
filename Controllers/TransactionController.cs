using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TransactionStore.Models;

namespace TransactionStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly IConfiguration _config;

    public TransactionController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<List<Transaction>>> GetTransactions()
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        IEnumerable<Transaction> heroes = await SelectAllTransactions(connection);
        return Ok(heroes);
    }

    [HttpGet("getTransaction/{transactionId}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int transactionId)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        var transaction = await connection.QueryFirstAsync<Transaction>("select * from Transactions where id = @Id",
                new { Id = transactionId });

        return Ok(transaction);
    }

    [HttpGet("getUserTransactions/{userId}")]
    public async Task<ActionResult<Transaction>> GetUserTransactions(int userId)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        IEnumerable<Transaction> userTransactions = await connection.QueryAsync<Transaction>
            ("select * from Transactions where UserId = @Id", new { Id = userId });

        return Ok(userTransactions);
    }

    [HttpGet("getUserAccountTransactions/{userId}/{accountId}")]
    public async Task<ActionResult<Transaction>> GetUserAccountTransactions(int userId, int accountId)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        IEnumerable<Transaction> userTransactions = await connection.QueryAsync<Transaction>
            ("select * from Transactions where UserId = @Id and AccountId = @accountId", new { Id = userId, AccountId = accountId });

        return Ok(userTransactions);
    }

    [HttpPost]
    public async Task<ActionResult<List<Transaction>>> CreateHero(Transaction transaction)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        await connection.ExecuteAsync("insert into Transactions (Id, UserId, AccountId, ReceiverId, TransactionAmount, TransactionDate)" +
            " values (@Id, @UserId, @AccountId, @ReceiverId, @TransactionAmount, @TransactionDate)", transaction);
        return Ok(await SelectAllTransactions(connection));
    }

    private static async Task<IEnumerable<Transaction>> SelectAllTransactions(SqlConnection connection)
    {
        return await connection.QueryAsync<Transaction>("select * from Transactions");
    }
}
