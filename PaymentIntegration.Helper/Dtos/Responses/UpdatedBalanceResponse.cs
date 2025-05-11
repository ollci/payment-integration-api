using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentIntegration.Helper.Dtos.Responses
{
    public class UpdatedBalanceResponse
    {
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }

        [JsonPropertyName("totalBalance")]
        public decimal TotalBalance { get; set; }

        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }

        [JsonPropertyName("blockedBalance")]
        public decimal BlockedBalance { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; set; }
    }
    
}
