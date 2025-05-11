using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceManagementAPI.Interfaces;
using FluentAssertions;
using Moq;
using PaymentIntegration.App.Services;
using PaymentIntegration.DataAccess.Interfaces;
using PaymentIntegration.Helper.Dtos.Responses;

namespace PaymentIntegration.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task ProcessPreOrderAsync_Should_Save_Order_And_Balance_When_Successful()
        {
            // Arrange
            var mockBalanceService = new Mock<IBalanceManagementService>();
            var mockOrderDb = new Mock<IOrderDbService>();
            var mockBalanceDb = new Mock<IBalanceDbService>();

            var fakeOrder = new OrderResponse { OrderId = "o1", Amount = 2, Status = "blocked", Timestamp = DateTime.UtcNow };
            var fakeBalance = new UpdatedBalanceResponse { UserId = "123123", BlockedBalance = 2, TotalBalance = 100, AvailableBalance = 98, Currency = "USD", LastUpdated = DateTime.UtcNow };

            var result = new PreOrderResponse { PreOrder = fakeOrder, UpdatedBalance = fakeBalance };

            mockBalanceService
                .Setup(x => x.PreOrderAsync("o1", 2))
                .ReturnsAsync(result);

            var service = new OrderService(mockBalanceService.Object, mockOrderDb.Object, mockBalanceDb.Object);

            // Act
            var response = await service.ProcessPreOrderAsync("o1", 2);

            // Assert
            response.Success.Should().BeTrue();
            response.Data.Should().Be("o1");

            mockOrderDb.Verify(x => x.AddAsync(fakeOrder), Times.Once);
            mockBalanceDb.Verify(x => x.AddAsync(fakeBalance, "o1"), Times.Once);
        }

        [Fact]
        public async Task ProcessPreOrderAsync_Should_Fail_When_Api_Returns_Null()
        {
            var mockBalanceService = new Mock<IBalanceManagementService>();
            var mockOrderDb = new Mock<IOrderDbService>();
            var mockBalanceDb = new Mock<IBalanceDbService>();

            mockBalanceService
                .Setup(x => x.PreOrderAsync(It.IsAny<string>(), It.IsAny<decimal>()))
                .ReturnsAsync((PreOrderResponse?)null);

            var service = new OrderService(mockBalanceService.Object, mockOrderDb.Object, mockBalanceDb.Object);

            var result = await service.ProcessPreOrderAsync("bad-order", 5);

            result.Success.Should().BeFalse();
            result.Message.Should().Contain("başarısız");

            mockOrderDb.Verify(x => x.AddAsync(It.IsAny<OrderResponse>()), Times.Never);
            mockBalanceDb.Verify(x => x.AddAsync(It.IsAny<UpdatedBalanceResponse>(), It.IsAny<string>()), Times.Never);

        }

     [Fact]
        public async Task CompleteOrderAsync_Should_Update_Order_And_Balance_When_Successful()
        {
            // Arrange
            var mockBalanceService = new Mock<IBalanceManagementService>();
            var mockOrderDb = new Mock<IOrderDbService>();
            var mockBalanceDb = new Mock<IBalanceDbService>();

            var fakeOrder = new OrderResponse
            {
                OrderId = "o1",
                Status = "completed",
                CompletedAt = DateTime.UtcNow,
                Timestamp = DateTime.UtcNow
            };

            var fakeBalance = new UpdatedBalanceResponse
            {
                UserId = "12312312",
                AvailableBalance = 100,
                BlockedBalance = 0,
                TotalBalance = 100,
                Currency = "USD",
                LastUpdated = DateTime.UtcNow
            };

            var response = new CompleteOrderResponse
            {
                Order = fakeOrder,
                UpdatedBalance = fakeBalance
            };

            mockBalanceService
                .Setup(x => x.CompleteOrderAsync("o1"))
                .ReturnsAsync(response);

            var service = new OrderService(mockBalanceService.Object, mockOrderDb.Object, mockBalanceDb.Object);

            // Act
            var result = await service.CompleteOrderAsync("o1");

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().Be("o1");

            mockOrderDb.Verify(x => x.UpdateAsync(fakeOrder), Times.Once);
            mockBalanceDb.Verify(x => x.UpdateAsync(fakeBalance, "o1"), Times.Once);
        }
        [Fact]
        public async Task CompleteOrderAsync_Should_Fail_When_Api_Returns_Null()
        {
            // Arrange
            var mockBalanceService = new Mock<IBalanceManagementService>();
            var mockOrderDb = new Mock<IOrderDbService>();
            var mockBalanceDb = new Mock<IBalanceDbService>();

            mockBalanceService
                .Setup(x => x.CompleteOrderAsync("o-bad"))
                .ReturnsAsync((CompleteOrderResponse?)null);

            var service = new OrderService(mockBalanceService.Object, mockOrderDb.Object, mockBalanceDb.Object);

            // Act
            var result = await service.CompleteOrderAsync("o-bad");

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("tamamlanamadı");

            mockOrderDb.Verify(x => x.UpdateAsync(It.IsAny<OrderResponse>()), Times.Never);
            mockBalanceDb.Verify(x => x.UpdateAsync(It.IsAny<UpdatedBalanceResponse>(), It.IsAny<string>()), Times.Never);
        }
    }
}

