using TransactionStore.Services.Interfaces;

namespace TransactionStore.Controllers;
public class AccountsController
{
    private readonly ITransactionService _transactionServices;

    public AccountsController(ITransactionService transactionServices)
    {
        _transactionServices = transactionServices;
    }

    //[HttpGet("{id}/balance")]
    //[ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    //public async Task<ActionResult<decimal>> GetBalanceByAccountId([FromRoute] long id)
    //{
    //    return Ok(await _transactionServices.GetBalanceByAccountId(id));
    //}

    //[HttpGet("{id}/transactions")]
    //[ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetTransactionsByAccountId([FromRoute] long id)
    //{
    //    var transactions = await _transactionServices.GetTransactionsByAccountId(id);

    //    var transactionsModel = _mapper.Map<List<TransactionResponse>>(transactions);

    //    _logger.LogInformation($"Controller: Transactions returned by accountId {id}");
    //    return Ok(transactionsModel);
    //}
}
