namespace Dealeron.SalesTaxes.Contract.Product
{
    public sealed class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
