using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Domain.Models;
using Dealeron.SalesTaxes.Domain.ValueObjects;
using Dealeron.SalesTaxes.Infrastructure.Services;
using Moq;
using Xunit;

namespace Dealeron.SalesTaxes.Infrastructure.UnitTests
{
    public class FileServiceTest
    {
        [Fact]
        public async Task Success_FileService_ParsingFile()
        {
            //Arrange
            var data = "1 Book at 12.49\r\n1 Book at 12.49\r\n1 Music CD at 14.99 \r\n1 Chocolate bar at 0.85";

            var expected = new BillingFile
            {
                Lines = new List<BillingFileLine>
                {
                    new BillingFileLine
                    {
                        Quantity = 1,
                        Description = "Book",
                        Price = 12.49m,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                CategoryId = 3,
                                Code = "exempt",
                                Name = "exempt",
                                TaxToApply = 0
                            }
                        }
                    },
                    new BillingFileLine
                    {
                        Quantity = 1,
                        Description = "Book",
                        Price = 12.49m,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                CategoryId = 3,
                                Code = "exempt",
                                Name = "exempt",
                                TaxToApply = 0
                            }
                        }
                    },
                    new BillingFileLine
                    {
                        Quantity = 1,
                        Description = "Music CD",
                        Price = 14.99m,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                CategoryId = 1,
                                Code = "basic",
                                Name = "basic",
                                TaxToApply = 0.1m
                            }
                        }
                    },
                    new BillingFileLine
                    {
                        Quantity = 1,
                        Description = "Chocolate bar",
                        Price = 0.85m,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                CategoryId = 3,
                                Code = "exempt",
                                Name = "exempt",
                                TaxToApply = 0
                            }
                        }
                    }
                }
            };

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Code = "basic",
                    Name = "basic",
                    TaxToApply = 0.1m
                },
                new Category
                {
                    CategoryId = 2,
                    Code = "import",
                    Name = "import",
                    TaxToApply = 0.05m
                },
                new Category
                {
                    CategoryId = 3,
                    Code = "exempt",
                    Name = "exempt",
                    TaxToApply = 0
                },
            };

            var repositoryMock = new Mock<ISalesTaxesRepository<Category>>();
            repositoryMock.Setup(x => x.GetAsync(0, 100, null))
                .ReturnsAsync(categories);
            var fileService = new FileService(repositoryMock.Object);

            //Act
            var actual = await fileService.ProcessFileAsync(data);

            //Assert
            repositoryMock.VerifyAll();
            Assert.NotNull(actual);
            Assert.True(expected.Lines.Count == actual.Lines.Count, "The number of lines does not match");
        }

        [Fact]
        public async Task Throw_InvalidDataException_Invalid_Number_Of_Words()
        {
            //Arrange
            var data = "1 Book 12.49\r\n1 Book at 12.49\r\n1 Music CD at 14.99 \r\n1 Chocolate bar at 0.85";

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Code = "basic",
                    Name = "basic",
                    TaxToApply = 0.1m
                },
                new Category
                {
                    CategoryId = 2,
                    Code = "import",
                    Name = "import",
                    TaxToApply = 0.05m
                },
                new Category
                {
                    CategoryId = 3,
                    Code = "exempt",
                    Name = "exempt",
                    TaxToApply = 0
                },
            };

            var repositoryMock = new Mock<ISalesTaxesRepository<Category>>();
            repositoryMock.Setup(x => x.GetAsync(0, 100, null))
                .ReturnsAsync(categories);
            var fileService = new FileService(repositoryMock.Object);

            //Act
            var ide = await Assert.ThrowsAsync<InvalidDataException>(async () => await fileService.ProcessFileAsync(data));

            //Assert
            repositoryMock.VerifyAll();
            Assert.Equal("Each line must have at least: quantity, description, and price. It seems the line 0 does not match with the expected format. Line value 1 Book 12.49", ide.Message);
        }

        [Fact]
        public async Task Throw_InvalidDataException_Missing_At_Word()
        {
            //Arrange
            var data = "1 Book @ 12.49\r\n1 Book at 12.49\r\n1 Music CD at 14.99 \r\n1 Chocolate bar at 0.85";

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Code = "basic",
                    Name = "basic",
                    TaxToApply = 0.1m
                },
                new Category
                {
                    CategoryId = 2,
                    Code = "import",
                    Name = "import",
                    TaxToApply = 0.05m
                },
                new Category
                {
                    CategoryId = 3,
                    Code = "exempt",
                    Name = "exempt",
                    TaxToApply = 0
                },
            };

            var repositoryMock = new Mock<ISalesTaxesRepository<Category>>();
            repositoryMock.Setup(x => x.GetAsync(0, 100, null))
                .ReturnsAsync(categories);
            var fileService = new FileService(repositoryMock.Object);

            //Act
            var ide = await Assert.ThrowsAsync<InvalidDataException>(async () => await fileService.ProcessFileAsync(data));

            //Assert
            repositoryMock.VerifyAll();
            Assert.Equal("Missing 'at' in line number 0.", ide.Message);
        }

        [Fact]
        public async Task Throw_InvalidDataException_Invalid_Price()
        {
            //Arrange
            var data = "1 Book at twelve\r\n1 Book at 12.49\r\n1 Music CD at 14.99 \r\n1 Chocolate bar at 0.85";

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Code = "basic",
                    Name = "basic",
                    TaxToApply = 0.1m
                },
                new Category
                {
                    CategoryId = 2,
                    Code = "import",
                    Name = "import",
                    TaxToApply = 0.05m
                },
                new Category
                {
                    CategoryId = 3,
                    Code = "exempt",
                    Name = "exempt",
                    TaxToApply = 0
                },
            };

            var repositoryMock = new Mock<ISalesTaxesRepository<Category>>();
            repositoryMock.Setup(x => x.GetAsync(0, 100, null))
                .ReturnsAsync(categories);
            var fileService = new FileService(repositoryMock.Object);

            //Act
            var ide = await Assert.ThrowsAsync<InvalidDataException>(async () => await fileService.ProcessFileAsync(data));

            //Assert
            repositoryMock.VerifyAll();
            Assert.Equal("Invalid parse to price with value twelve at line number 0.", ide.Message);
        }

        [Fact]
        public async Task Throw_InvalidDataException_Invalid_Quantity()
        {
            //Arrange
            var data = "One Book at 12.49\r\n1 Book at 12.49\r\n1 Music CD at 14.99 \r\n1 Chocolate bar at 0.85";

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Code = "basic",
                    Name = "basic",
                    TaxToApply = 0.1m
                },
                new Category
                {
                    CategoryId = 2,
                    Code = "import",
                    Name = "import",
                    TaxToApply = 0.05m
                },
                new Category
                {
                    CategoryId = 3,
                    Code = "exempt",
                    Name = "exempt",
                    TaxToApply = 0
                },
            };

            var repositoryMock = new Mock<ISalesTaxesRepository<Category>>();
            repositoryMock.Setup(x => x.GetAsync(0, 100, null))
                .ReturnsAsync(categories);
            var fileService = new FileService(repositoryMock.Object);

            //Act
            var ide = await Assert.ThrowsAsync<InvalidDataException>(async () => await fileService.ProcessFileAsync(data));

            //Assert
            repositoryMock.VerifyAll();
            Assert.Equal("Invalid quantity value:One at line number: 0.", ide.Message);
        }
    }
}