namespace Dealeron.SalesTaxes.Contract.Category
{
    public sealed class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal TaxToApply { get; set; }
    }
}
