using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Data.Domain
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }


        public Balance? Balance { get; set; }
    }
}
