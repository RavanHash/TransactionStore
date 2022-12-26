namespace TransactionStore.Models;
public static class RateModel
{
    public static Dictionary<string, decimal> CurrencyRates = new Dictionary<string, decimal>()
    {
        { "USD", 0.59m },
        { "EUR", 0.55m },
        { "RUB", 40.21m },
    };

    public static string BaseCurrency = "AZN";
}
