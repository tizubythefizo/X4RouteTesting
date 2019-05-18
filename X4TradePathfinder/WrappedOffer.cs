using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4TradePathfinder
{
    public class WrappedOffer
    {
        public TradeOffer Offer { get; set; }
        public bool IsBuy { get; set; }
        public TradeOffer AssociatedOffer { get; set; }
    }
}
