namespace TransactionStore.Models;
public class RateModel
{
    public static Dictionary<string, decimal> CurrencyRates { get; set; }
    public static string BaseCurrency { get; set; }
}
