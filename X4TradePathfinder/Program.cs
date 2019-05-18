using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace X4TradePathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var maker = new TradeMaker();
            var routes = maker.GetSimpleTestTrades();

            Console.WriteLine("Route Pathing Starting...");
            Console.WriteLine("Generated raw route looks like:");

            foreach (var trade in routes)
            {
                Console.WriteLine("--Buy ware: ({0}) from station: ({1}) in sector: ({2}) and sell to station: ({3}) in sector: ({4})", trade.SellOffer.OfferWare.Name, trade.SellOffer.Seller.Name, trade.SellOffer.Seller.HomeSector.Name, trade.BuyOffer.Buyer.Name, trade.BuyOffer.Buyer.HomeSector.Name);
            }

            //Call router
            var pather = new RoutePather();

            var timer = Stopwatch.StartNew();
            var pathResult = pather.Pathfind(routes);
            timer.Stop();

            Console.WriteLine();
            Console.WriteLine("Pathing complete in {0}ms, offer list as follows...", timer.Elapsed.TotalMilliseconds);

            foreach (var offer in pathResult)
            {
                var buyOrSellWords = "";
                var stationName = "";
                var sectorName = "";

                if (offer.Buyer != null)
                {
                    buyOrSellWords = "Selling to";
                    stationName = offer.Buyer.Name;
                    sectorName = offer.Buyer.HomeSector.Name;
                }
                else
                {
                    buyOrSellWords = "Buying from";
                    stationName = offer.Seller.Name;
                    sectorName = offer.Seller.HomeSector.Name;
                }

                Console.WriteLine("-- Ware ({0}) {1} ({2}) in {3}", offer.OfferWare.Name, buyOrSellWords, stationName, sectorName);
            }

            Console.WriteLine();
            Console.WriteLine("Testing alternative pather, same dataset");

            var secondPather = new RoutePather2();

            var otherTimer = Stopwatch.StartNew();
            //TODO: Take the result
            secondPather.FindPath(routes);
            otherTimer.Stop();

            Console.WriteLine("Pathing complete in {0}ms, offer list as follows...", otherTimer.Elapsed.TotalMilliseconds);

            Console.ReadLine();
        }
    }
}
