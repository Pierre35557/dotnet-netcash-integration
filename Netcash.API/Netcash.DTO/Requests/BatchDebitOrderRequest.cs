using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netcash.DTO.Requests
{
    public class BatchDebitOrderRequest
    {
        public BatchDebitOrderRequest() 
        {
            Users = new List<BatchDebitOrderUserRequest>();
        } 

        public string BatchReferenceNo { get; set; }

        //yyyyMMdd
        public string DebitOrderDate { get; set; }

        public List<BatchDebitOrderUserRequest> Users { get; set; }
    }

    public class BatchDebitOrderUserRequest
    {
        /// <summary>
        /// User account number
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// Amount in cents
        /// </summary>
        public long Amount { get; set; }
    }
}
