using LoggerService;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models;
using TransactionStore.API.Services;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    static HttpClient httpClient = new HttpClient();
    private readonly ILoggerManager _logger;
    private readonly ITransactionService _transactionServices;
    //private readonly IMapper _mapper;

    public TransactionController(ILoggerManager logger, ITransactionService transactionServices) //, Mapper mapper)
    {
        _logger = logger;
        //_mapper = mapper;
        _transactionServices = transactionServices;
    }

    [HttpPost("deposit")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<TransactionModel> AddDeposit([FromBody] TransactionRequest transactionRequest)
    {
        Console.WriteLine(1);
        var transaction = await _transactionServices.AddDeposit(CustomMapper.MapTransactionRequestModel(transactionRequest));

        return transaction;
    }

    [HttpPost("withdraw")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<TransactionModel>> Withdraw([FromBody] TransactionModel transaction, int transactionId)
    {
        _logger.LogInformation($"Controller: Call method Withdraw, userId {transaction.UserId}, amount {transaction.TransactionAmount}, {transaction.Currency}");

        return Ok(await _transactionServices.Withdraw(transaction, transactionId));
    }

    [HttpPost("transfer")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<List<TransactionModel>>> AddTransfer([FromBody] List<TransactionModel> transferModels)
    {
        _logger.LogInformation($"Controller: Call method AddTransfer. Sender: userId {transferModels[0].UserId}, Recipient: account id {transferModels[0].ReceiverId}");

        return await _transactionServices.AddTransfer(transferModels);
    }


    [HttpGet("all")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    public async Task<ActionResult<TransactionModel>> GetTransactions()
    {
        _logger.LogInformation($"Controller: Call method GetTransactions, all ");

        return Ok(await _transactionServices.GetAllTransactions());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<TransactionModel>> GetTransactionById([FromRoute] int id)
    {
        _logger.LogInformation($"Controller: Call method GetTransactionById, transaction id {id} ");

        return Ok(await _transactionServices.GetTransactionById(id));
    }
}
