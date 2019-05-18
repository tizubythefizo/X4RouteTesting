using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4TradePathfinder
{
    public class TradeOffer
    {
        public Station Buyer { get; set; }
        public Station Seller { get; set; }
        public Ware OfferWare { get; set; }
    }
}
