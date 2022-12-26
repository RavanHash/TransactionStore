using LoggerService;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Services;
public class CalculationService : ICalculationService
{
    private readonly ILoggerManager _logger;
    private readonly IRateService _rateService;

    public CalculationService(ILoggerManager logger, IRateService rateService)
    {
        _logger = logger;
        _rateService = rateService;
    }

    public async Task<List<TransactionModel>> ConvertCurrency(List<TransactionModel> transferModels)
    {
        var senderIndex = 0;
        var recipientIndex = 1;

        _logger.LogInformation("Business layer: Call GetCurrencyRate method");
        var crossRate = _rateService.GetCurrencyRate(transferModels[senderIndex].Currency.ToString(), transferModels[recipientIndex].Currency.ToString());

        if (transferModels[0].Currency.ToString() == RateModel.BaseCurrency || transferModels[1].Currency.ToString() == RateModel.BaseCurrency)
        {
            _logger.LogInformation($"Business layer: Converting {transferModels[senderIndex].Currency} to {transferModels[recipientIndex].Currency} amount {transferModels[senderIndex].TransactionAmount}");
            transferModels[recipientIndex].TransactionAmount = transferModels[senderIndex].Currency.ToString() == RateModel.BaseCurrency ?
            transferModels[senderIndex].TransactionAmount * crossRate : transferModels[senderIndex].TransactionAmount / crossRate;
        }
        else
        {
            _logger.LogInformation($"Business layer: Converting {transferModels[0].Currency} to {transferModels[recipientIndex].Currency} amount {transferModels[senderIndex].TransactionAmount}");
            transferModels[recipientIndex].TransactionAmount = Math.Round((transferModels[senderIndex].TransactionAmount * crossRate),
                                                                       4, MidpointRounding.ToNegativeInfinity);
        }
        transferModels[senderIndex].TransactionAmount *= -1;

        _logger.LogInformation("Business layer: Transfers converted returned");
        return transferModels;
    }
}
