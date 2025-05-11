using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentIntegration.Helper.Dtos.Responses
{
    public class PreOrderResponse
    {
        [JsonPropertyName("preOrder")]
        public OrderResponse PreOrder { get; set; } = new();

        [JsonPropertyName("updatedBalance")]
        public UpdatedBalanceResponse UpdatedBalance { get; set; } = new();
    }
}
