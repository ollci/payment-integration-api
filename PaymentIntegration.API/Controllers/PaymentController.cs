using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentIntegration.App.Interfaces;
using PaymentIntegration.Helper.Dtos.Requests;
using PaymentIntegration.Helper.Dtos.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public PaymentController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Ön sipariş başlatır (bakiye blokeler ve veritabanına ekler)
        /// </summary>
        [HttpPost("preorder")]
        [SwaggerOperation(Summary = "Siparişi başlatır ve bakiyeyi blokeler")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PreOrder([FromQuery] PreOrderQuery query)
        {
            var result = await _orderService.ProcessPreOrderAsync(query.OrderId, query.Amount);
            return Ok(result);
        }

        /// <summary>
        /// Siparişi tamamlar ve blokeli bakiyeyi günceller
        /// </summary>
        [HttpPost("complete")]
        [SwaggerOperation(Summary = "Siparişi tamamlar ve bakiyeyi günceller")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Complete([FromQuery] CompleteOrderQuery query)
        {
            var result = await _orderService.CompleteOrderAsync(query.OrderId);
            return Ok(result);
        }
    }
}
