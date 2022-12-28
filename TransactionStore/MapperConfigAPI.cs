using AutoMapper;
using TransactionStore.API.Models;
using TransactionStore.Models;

namespace TransactionStore;

public class MapperConfigAPI : Profile
{
    public MapperConfigAPI()
    {

        CreateMap<TransactionRequest, TransactionModel>();
        CreateMap<TransactionModel, TransactionResponse>();

        //CreateMap<TransactionModel, TransactionResponse>();

        //CreateMap<TransactionTransferRequest, List<TransactionModel>>()
        //    .ConvertUsing<TransferRequestMapper>();

        //CreateMap<Dictionary<DateTime, List<TransactionModel>>, List<TransactionResponse>>()
        //   .ConvertUsing<TransactionResponseMapper>();
    }
}
