using Dealeron.SalesTaxes.Domain.ValueObjects;
using Dealeron.SalesTaxes.Infrastructure.Services;
using Xunit;

namespace Dealeron.SalesTaxes.Infrastructure.UnitTests
{
    public class BillingServiceTest
    {
        [Fact]
        public async Task Success_CreateBillingAsync()
        {
            //Arrange
            var billingFile = new BillingFile
            {
                Lines = new List<BillingFileLine>
                {
                    new BillingFileLine
                    {
                        Description = "Book",
                        Price = 12.49m,
                        TaxPrice = 0,
                    },
                    new BillingFileLine
                    {
                        Description = "Book",
                        Price = 12.49m,
                        TaxPrice = 0,
                    },
                    new BillingFileLine
                    {
                        Description = "Music CD",
                        Price = 14.99m,
                        TaxPrice = 1.5m,
                    },
                    new BillingFileLine
                    {
                        Description = "Chocolate bar",
                        Price = 0.85m,
                        TaxPrice = 0,
                    },
                }
            };

            var expected = "Book: 24.98 (2 @ 12.49)\r\nMusic CD: 16.49 \r\nChocolate bar: 0.85 \r\nSales Taxes: 1.5\r\nTotal: 42.32";

            var billingService = new BillingService();

            //Act
            var actual = await billingService.CreateBillAsync(billingFile);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }
}
