﻿namespace TransactionStore.Models;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AccountId { get; set; }
    public int ReceiverId { get; set; }
    public decimal TransactionAmount { get; set; }

    //public TranType TransactionType { get; set; }

    public DateTime TransactionDate { get; set; }

    public enum TranType
    {
        Deposit,
        Withdraw,
        Transfer
    }
}
