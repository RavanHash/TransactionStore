using Microsoft.AspNetCore.Mvc;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Controllers;
public class UsersController : ControllerBase
{
    private readonly ITransactionService _transactionServices;

    public UsersController(ITransactionService transactionServices)
    {
        _transactionServices = transactionServices;
    }

    [HttpGet("{userId}/balance")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> GetBalanceByUserId([FromRoute] int userId)
    {
        return Ok(await _transactionServices.GetBalanceByUserId(userId));
    }

    [HttpGet("{userId}/transactions")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionsByAccountId([FromRoute] int userId)
    {
        return Ok(await _transactionServices.GetTransactionsByUserId(userId));
    }
}
