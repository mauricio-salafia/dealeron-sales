﻿using System.Diagnostics.CodeAnalysis;

namespace Dealeron.SalesTaxes.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}