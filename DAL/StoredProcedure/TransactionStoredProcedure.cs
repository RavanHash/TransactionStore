﻿using Dapper;
using System.Data.SqlClient;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.DAL.StoredProcedure;
public class TransactionStoredProcedure : ITransactionStoredProcedure
{
    private readonly IConfiguration _config;

    public TransactionStoredProcedure(IConfiguration config)
    {
        _config = config;
    }

    public async void InsertTransaction(TransactionModel transaction)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        await connection.ExecuteAsync("insert into Transactions (Id, UserId, AccountId, ReceiverId, TransactionAmount, TransactionDate)" +
           " values (@Id, @UserId, @AccountId, @ReceiverId, @TransactionAmount, @TransactionDate)", transaction);
    }


    public async Task<IEnumerable<TransactionModel>> SelectAllTransactions()
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        return await connection.QueryAsync<TransactionModel>("select * from Transactions");
    }
    public async Task<TransactionModel> SelectTransactionById(int transactionId)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        return await connection.QueryFirstAsync<TransactionModel>("select * from Transactions where id = @Id",
                new { Id = transactionId });
    }

    public async Task<IEnumerable<TransactionModel>> SelectAllTransactionsByUserId(int userId)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        return await connection.QueryAsync<TransactionModel>
            ("select * from Transactions where UserId = @Id", new { Id = userId });
    }

    public async Task<IEnumerable<TransactionModel>> SelectAllTransactionsByAccountId(int userId, int accountId)
    {
        using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        return await connection.QueryAsync<TransactionModel>
            ("select * from Transactions where UserId = @Id and AccountId = @accountId", new { Id = userId, AccountId = accountId });
    }
}