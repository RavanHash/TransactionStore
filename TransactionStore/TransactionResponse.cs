using static TransactionStore.Models.TransactionModel;

namespace TransactionStore;

public class TransactionResponse
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public DateTime Date { get; set; }
    public TranType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public CurrencyType Currency { get; set; }
}