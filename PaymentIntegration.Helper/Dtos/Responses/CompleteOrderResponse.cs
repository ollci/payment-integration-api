using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentIntegration.Helper.Dtos.Responses
{
    public class CompleteOrderResponse
    {
        [JsonPropertyName("order")]
        public OrderResponse Order { get; set; } = new();

        [JsonPropertyName("updatedBalance")]
        public UpdatedBalanceResponse UpdatedBalance { get; set; } = new();
    }
}
