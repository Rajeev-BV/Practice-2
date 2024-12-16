using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2
{
    public class TaxSlabs
    {
        public double SlabAmount { get; set; }
        public double taxAmount { get; set; }
        public TaxSlabs(double slabAmount, double taxAmount)
        {
            SlabAmount = slabAmount;
            this.taxAmount = taxAmount;
        }
    }
}
