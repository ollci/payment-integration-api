using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Helper.Dtos.Requests
{
    public class PreOrderQuery
    {
        [Required(ErrorMessage = "orderId boş olamaz.")]
        [MinLength(3, ErrorMessage = "orderId en az 3 karakter olmalıdır.")]
        public string OrderId { get; set; } = string.Empty;

        [Range(1, 1000000, ErrorMessage = "amount 1 ile 1.000.000 arasında olmalıdır.")]
        public decimal Amount { get; set; }
    }
}
