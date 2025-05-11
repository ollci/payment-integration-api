using BalanceManagementAPI.Interfaces;
using FluentAssertions;
using Moq;
using PaymentIntegration.App.Services;
using PaymentIntegration.DataAccess.Interfaces;
using PaymentIntegration.Helper.Dtos.Responses;

namespace PaymentIntegration.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetAvailableProductsAsync_Should_Return_Only_Stocked_Products()
        {
            // Arrange
            var mockBalanceService = new Mock<IBalanceManagementService>();
            var mockProductDbService = new Mock<IProductDbService>();

            var products = new List<ProductListResponse>
        {
            new() { Id = "p1", Name = "A", Stock = 5 },
            new() { Id = "p2", Name = "B", Stock = 0 }, // stokta olmayan
            new() { Id = "p3", Name = "C", Stock = 3 }
        };

            mockBalanceService
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(products);

            var productService = new ProductService(mockBalanceService.Object, mockProductDbService.Object);

            // Act
            var result = await productService.GetAvailableProductsAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Data.Should().OnlyContain(p => p.Stock > 0);


            mockProductDbService.Verify(x => x.AddOrUpdateAsync(It.IsAny<ProductListResponse>()), Times.Exactly(3));
        }
    }
}
