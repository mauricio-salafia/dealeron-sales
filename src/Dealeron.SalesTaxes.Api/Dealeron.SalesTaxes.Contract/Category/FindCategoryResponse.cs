using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealeron.SalesTaxes.Contract.Category
{
    public class FindCategoryResponse
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal TaxToApply { get; set; }
    }
}
