using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentIntegration.App.Interfaces;
using PaymentIntegration.Helper.Dtos.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Stokta bulunan ürünleri listeler ve veritabanına kaydeder.
        /// </summary>
        [HttpGet("available")]
        [SwaggerOperation(Summary = "Stokta bulunan ürünleri getirir")]
        [ProducesResponseType(typeof(ApiResponse<List<ProductListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<List<ProductListResponse>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailableProducts()
        {
            var result = await _productService.GetAvailableProductsAsync();
            return Ok(result);
        }
    }
}
