﻿using LoggerService;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Services;
public class RateService : IRateService
{
    private readonly ILoggerManager _logger;

    public RateService(ILoggerManager logger) => _logger = logger;
    private readonly object _locker = new object();
    public void SaveCurrencyRate(Dictionary<string, decimal> rates)
    {
        lock (_locker)
        {
            _logger.LogInformation("Business layer: Convert to the dictionary currency rates wihtout base currency");
            RateModel.CurrencyRates = rates.ToDictionary(t => t.Key.Substring(3), t => t.Value);

            _logger.LogInformation("Business layer: Find base currency");
            RateModel.BaseCurrency = rates.GroupBy(k => k.Key.Remove(3))
                .FirstOrDefault()!
                .Key;
        }
    }

    public decimal GetCurrencyRate(string currencyFirst, string currencySecond)
    {
        var result = 1m;

        lock (_locker)
        {
            _logger.LogInformation("Business layer: Call method GetRate");

            var rates = GetRate();

            if (currencyFirst != currencySecond)
            {
                if (RateModel.BaseCurrency == currencyFirst || RateModel.BaseCurrency == currencySecond)
                {
                    _logger.LogInformation("Business layer: Calculate currency rate");
                    result = rates.ContainsKey(currencyFirst) is true ? rates[currencyFirst] : rates[currencySecond];
                }
                else
                {
                    _logger.LogInformation("Business layer: Calculate currency rate");
                    result = rates[currencySecond] / rates[currencyFirst];
                }
            }
            return result;
        }
    }


    public Dictionary<string, decimal> GetRate()
    {
        _logger.LogInformation("Business layer: Currency rates returned");

        return RateModel.CurrencyRates;
    }
}
