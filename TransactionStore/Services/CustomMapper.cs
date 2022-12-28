using TransactionStore.API.Models;
using TransactionStore.Models;

namespace TransactionStore.API.Services;
public static class CustomMapper
{
    public static TransactionModel MapTransactionRequestModel(TransactionRequest transactionRequest)
    {
        TransactionModel transactionModel = new TransactionModel();

        transactionModel.UserId = transactionRequest.UserId;
        transactionModel.Currency = transactionRequest.Currency;
        transactionModel.TransactionAmount = transactionRequest.TransactionAmount;
        transactionModel.TransactionDate = DateTime.Now;
        transactionModel.TransactionType = TransactionModel.TranType.Deposit;


        return transactionModel;
    }
}
