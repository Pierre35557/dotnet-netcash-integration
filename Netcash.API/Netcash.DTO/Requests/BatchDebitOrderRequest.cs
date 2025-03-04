using System.ComponentModel;

namespace Netcash.DTO.Requests
{
    public class BatchDebitOrderRequest
    {
        public BatchDebitOrderRequest() 
        {
            Users = new List<BatchDebitOrderUserRequest>();
        }

        [Description("Unique reference for tracking the batch in Netcash.")]
        public string BatchReferenceNo { get; set; }

        [Description("Date of debit order processing (CCYYMMDD format).")]
        public string DebitOrderDate { get; set; }

        [Description("List of user debit order transactions.")]
        public List<BatchDebitOrderUserRequest> Users { get; set; }
    }

    [Description("Represents a single debit order transaction.")]
    public class BatchDebitOrderUserRequest
    {
        [Description("User account number to debit.")]
        public string AccountNo { get; set; }

        [Description("Amount to debit (in cents, e.g., 10000 for R100.00).")]
        public long Amount { get; set; }
    }
}
