using Microsoft.AspNetCore.Mvc;
using TransactionStore.Models;

namespace TransactionStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private static List<Transaction> _transactions = new List<Transaction>()
        {
            new Transaction
            {
                UserId = 1,
                AccountId = 1,
                ReceiverId = 1,
                TransactiontId = 1
            },
            new Transaction
            {
                UserId = 2,
                AccountId = 2,
                ReceiverId = 2,
                TransactiontId = 2
            }
        };


        [HttpGet("getAll")]
        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
        
        [HttpPost("add")]
        public List<Transaction> PostTransactions(Transaction transaction)
        {
            _transactions.Add(transaction);
            return _transactions;
        }

    }
}
