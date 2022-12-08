namespace TransactionStore.Models
{
    public class Transaction
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public int ReceiverId { get; set; }
        public int TransactiontId { get; set; }
        public enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }
    }
}
