using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealeron.SalesTaxes.Domain.ValueObjects
{
    public class BillingFile
    {
        public List<BillingFileLine> Lines { get; set; }
    }
}
