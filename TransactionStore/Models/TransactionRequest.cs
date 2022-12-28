using static TransactionStore.Models.TransactionModel;

namespace TransactionStore.API.Models;

public class TransactionRequest
{
    public string UserId { get; set; }
    public CurrencyType Currency { get; set; }
    public decimal TransactionAmount { get; set; }
}