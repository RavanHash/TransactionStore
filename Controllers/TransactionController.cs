using Microsoft.AspNetCore.Mvc;
using TransactionStore.Models;
using TransactionStore.Services.Interfaces;

namespace TransactionStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionServices;

    public TransactionController(ITransactionService transactionServices)
    {
        _transactionServices = transactionServices;
    }


    [HttpPost("deposit")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<TransactionModel>> AddDeposit([FromBody] TransactionModel transaction, int transactionId)
    {
        return Ok(await _transactionServices.AddDeposit(transaction, transactionId));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<TransactionModel>> GetTransactions()
    {
        return Ok(await _transactionServices.GetAllTransactions());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<TransactionModel>> GetTransactionById([FromRoute] int id)
    {
        return Ok(await _transactionServices.GetTransactionById(id));
    }
}
