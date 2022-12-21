namespace Dealeron.SalesTaxes.Contract.Category
{
    public sealed class CreateCategoryRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal TaxToApply { get; set; }
    }
}