namespace TransactionStore.Services.Interfaces;
public interface IRateService
{
    public void SaveCurrencyRate(Dictionary<string, decimal> rates);
    public decimal GetCurrencyRate(string currencyFirst, string currencySecond);
    public Dictionary<string, decimal> GetRate();
}
