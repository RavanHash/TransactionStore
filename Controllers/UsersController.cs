using LoggerService;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly ITransactionService _transactionServices;

    public UsersController(ILoggerManager logger, ITransactionService transactionServices)
    {
        _logger = logger;
        _transactionServices = transactionServices;
    }

    [HttpGet("{userId}/balance")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> GetBalanceByUserId([FromRoute] int userId)
    {
        _logger.LogInformation($"Controller: Call method GetBalanceByAccountId {userId}");

        return Ok(await _transactionServices.GetBalanceByUserId(userId));
    }

    [HttpGet("{userId}/transactions")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionsByUserId([FromRoute] int userId)
    {
        _logger.LogInformation($"Controller: Call method GetTransactionsByUserId {userId}");

        return Ok(await _transactionServices.GetTransactionsByUserId(userId));
    }
}
