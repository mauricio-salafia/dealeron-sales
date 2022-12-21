using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Domain.Models;
using Dealeron.SalesTaxes.Domain.ValueObjects;

namespace Dealeron.SalesTaxes.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly ISalesTaxesRepository<Category> _repository;
        private IEnumerable<string> _exemptKeyWords;
        private IEnumerable<string> _dutyKeyWords;

        public FileService(ISalesTaxesRepository<Category> repository)
        {
            _repository = repository;
            _exemptKeyWords = new List<string> { "chocolate", "chocolates", "book", "headache", "stomachache" };
            _dutyKeyWords = new List<string> { "imported" };

        }

        public async Task<BillingFile> ProcessFileAsync(string dataFile, CancellationToken cancellationToken = default(CancellationToken))
        {
            var categories = await _repository.GetAsync(0, 100);
            var billingFileLines = GetBillingFileLines(dataFile);
            billingFileLines = UpdateBillingFileLinesWithCategory(billingFileLines, categories);
            billingFileLines = UpdateTaxPrices(billingFileLines);
            var result = new BillingFile();
            result.Lines = billingFileLines;
            return await Task.FromResult(result);

        }

        private List<BillingFileLine> UpdateTaxPrices(List<BillingFileLine> billingFileLines)
        {
            foreach (var billingFileLine in billingFileLines)
            {
                billingFileLine.TaxPrice = UpdateTaxPrice(billingFileLine);
            }

            return billingFileLines;
        }

        private decimal UpdateTaxPrice(BillingFileLine billingFileLine)
        {
            decimal taxFee = billingFileLine.Categories.Sum(x => x.TaxToApply);
            decimal taxPrice = billingFileLine.Price * taxFee;
            taxPrice = Math.Ceiling(taxPrice / 0.05m) * 0.05m;
            return taxPrice;
        }

        private List<BillingFileLine> UpdateBillingFileLinesWithCategory(List<BillingFileLine> billingFileLines,
            IEnumerable<Category> categories)
        {
            foreach (var billingFileLine in billingFileLines)
            {
                billingFileLine.Categories = GetCategoriesFromDescription(billingFileLine.Description, categories);
            }

            return billingFileLines;
        }

        private List<Category> GetCategoriesFromDescription(string productDescription, IEnumerable<Category> categories)
        {
            var result = new List<Category>();
            var words = productDescription.ToLowerInvariant().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (!words.Any(w => _exemptKeyWords.Contains(w)))
            {
                result.Add(categories.Single(c => c.Code == "basic"));
            }
            else
            {
                result.Add(categories.Single(c => c.Code == "exempt"));
            }

            if (words.Any(w => _dutyKeyWords.Contains(w)))
            {
                result.Add(categories.Single(c => c.Code == "import"));
            }

            return result;
        }

        private List<BillingFileLine> GetBillingFileLines(string dataFile)
        {
            var result = new List<BillingFileLine>();
            var lines = dataFile.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < lines.Length; index++)
            {
                var billingFileLine = CreateBillingFileLine(lines[index], index);
                result.Add(billingFileLine);
            }

            return result;
        }

        private BillingFileLine CreateBillingFileLine(string dataLine, int lineNumber)
        {
            var items = dataLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (items.Length < 4)
            {
                throw new InvalidDataException($"Each line must have at least: quantity, description, and price. " +
                    $"It seems the line {lineNumber} does not match with the expected format. " +
                    $"Line value {dataLine}");
            }

            var result = new BillingFileLine();
            result.Quantity = GetQuantity(items[0], lineNumber);
            var descriptionAndPrice = GetDescriptionAndPrice(items, lineNumber);
            result.Description = descriptionAndPrice.Item1;
            result.Price = descriptionAndPrice.Item2;
            return result;
        }

        private int GetQuantity(string itemData, int lineNumber)
        {
            int result = 0;
            if (!int.TryParse(itemData, out result))
            {
                throw new InvalidDataException($"Invalid quantity value:{itemData} at line number: {lineNumber}.");
            }

            return result;
        }

        private (string, decimal) GetDescriptionAndPrice(string[] dataLine, int lineNumber)
        {
            decimal price = 0;
            int? index = dataLine.Select((item, index) => new
            {
                item = item,
                index = index
            })
                .FirstOrDefault(x => string.Compare(x.item, "at", true) == 0)?.index;

            if (index == null)
            {
                throw new InvalidDataException($"Missing 'at' in line number {lineNumber}.");
            }

            var priceStr = dataLine.ElementAt(index.Value + 1);
            if (!decimal.TryParse(priceStr, out price))
            {
                throw new InvalidDataException($"Invalid parse to price with value {priceStr} at line number {lineNumber}.");
            }

            var descriptionElemnts = dataLine.Skip(1).Take(index.Value - 1);
            var description = string.Join(" ", descriptionElemnts);
            return (description, price);
        }
    }
}
