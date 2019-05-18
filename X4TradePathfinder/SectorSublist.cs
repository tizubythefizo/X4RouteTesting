using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4TradePathfinder
{
    public class SectorSublist
    {
        public int IndexOfFirstBuyOffer { get; set; }
        public List<WrappedOffer> OfferList { get; set; }
    }
}
