using TransactionStore.Models;

namespace TransactionStore.Services.Interfaces;
public interface ICalculationService
{
    public Task<List<TransactionModel>> ConvertCurrency(List<TransactionModel> transferModels);
}
