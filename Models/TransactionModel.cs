using System.ComponentModel.DataAnnotations;

namespace TransactionStore.Models;

public class TransactionModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? ReceiverId { get; set; }
    public decimal TransactionAmount { get; set; }

    [EnumDataType(typeof(TranType))]
    public TranType TransactionType { get; set; }

    [EnumDataType(typeof(CurrencyType))]
    public CurrencyType Currency { get; set; }
    public DateTime TransactionDate { get; set; }

    public enum TranType
    {
        Deposit = 1,
        Withdraw,
        Transfer
    }

    public enum CurrencyType
    {
        AZN = 1,
        USD,
        EUR,
        RUB
    }
}
