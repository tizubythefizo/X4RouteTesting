using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4TradePathfinder
{
    public class TradeMaker
    {
        public List<TradeRoute> GetSimpleTestTrades()
        {
            var result = new List<TradeRoute>();

            var tradeSell1 = new TradeOffer {Seller = StationDb.C_Station1, OfferWare = WaresDB.MajaSnails };
            var tradeBuy1 = new TradeOffer { Buyer = StationDb.A_Station2, OfferWare = WaresDB.MajaSnails };
            var tradeRoute1 = new TradeRoute { BuyOffer = tradeBuy1, SellOffer = tradeSell1 };
            result.Add(tradeRoute1);

            var tradeSell2 = new TradeOffer { Seller = StationDb.A_Station1, OfferWare = WaresDB.MissileComponents };
            var tradeBuy2 = new TradeOffer { Buyer = StationDb.C_Station3, OfferWare = WaresDB.MissileComponents };
            var tradeRoute2 = new TradeRoute { BuyOffer = tradeBuy2, SellOffer = tradeSell2 };
            result.Add(tradeRoute2);

            var tradeSell3 = new TradeOffer { Seller = StationDb.D_Station1, OfferWare = WaresDB.EngineParts };
            var tradeBuy3 = new TradeOffer { Buyer = StationDb.B_Station1, OfferWare = WaresDB.EngineParts };
            var tradeRoute3 = new TradeRoute { BuyOffer = tradeBuy3, SellOffer = tradeSell3 };
            result.Add(tradeRoute3);

            var tradeSell4 = new TradeOffer { Seller = StationDb.A_Station3, OfferWare = WaresDB.PlasmaConductors };
            var tradeBuy4 = new TradeOffer { Buyer = StationDb.D_Station2, OfferWare = WaresDB.PlasmaConductors };
            var tradeRoute4 = new TradeRoute { BuyOffer = tradeBuy4, SellOffer = tradeSell4 };
            result.Add(tradeRoute4);

            var tradeSell5 = new TradeOffer { Seller = StationDb.B_Station2, OfferWare = WaresDB.SojaBeans };
            var tradeBuy5 = new TradeOffer { Buyer = StationDb.C_Station2, OfferWare = WaresDB.SojaBeans };
            var tradeRoute5 = new TradeRoute { BuyOffer = tradeBuy5, SellOffer = tradeSell5 };
            result.Add(tradeRoute5);

            return result;
        }
    }
}
