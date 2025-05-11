using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Data.Domain
{
    public class Balance
    {
        [Key]
        public string OrderId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public decimal TotalBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal BlockedBalance { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime? LastUpdated { get; set; }


        public Order? Order { get; set; }
    }
}
