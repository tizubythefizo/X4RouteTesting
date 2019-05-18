using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4TradePathfinder
{
    public class RoutePather
    {        
        public List<TradeOffer> Pathfind(List<TradeRoute> rawRoutes)
        {
            var result = new List<TradeOffer>();
            var temp = new List<List<TradeOffer>>();

            //Can't use foreach, use regular for in xml
            foreach (var trade in rawRoutes)
            {
                // Result list is empty
                if (result.Count == 0)
                {
                    result.Add(trade.SellOffer);
                    result.Add(trade.BuyOffer);
                    continue;
                }

                var buyingFrom_offer = trade.SellOffer;
                var sellingTo_offer = trade.BuyOffer;

                // Check if the sell sector is already in the result list
                var sellingToIndexes = LocationOfSectorInRoute(trade.BuyOffer.Buyer.HomeSector, result);
                var buyingFromIndexes = LocationOfSectorInRoute(trade.SellOffer.Seller.HomeSector, result);

                // Is it time to merge in the temp list?
                for (int j = 0; j < temp.Count; j++)
                {
                    var tempList = temp[j];
                    var tempSellingToIndexes = LocationOfSectorInRoute(trade.BuyOffer.Buyer.HomeSector, tempList);
                    var tempBuyingFromIndexes = LocationOfSectorInRoute(trade.SellOffer.Seller.HomeSector, tempList);
                    var delete_list = false;

                    // If we have a sector from the result list and the temp list, merge them
                    if ((tempSellingToIndexes.Count > 0 || tempBuyingFromIndexes.Count > 0) &&
                        (sellingToIndexes.Count > 0 || buyingFromIndexes.Count > 0))
                    {
                        delete_list = true;
                        // Merge with buy taking priority
                        if (buyingFromIndexes.Count > 0)
                        {
                            for (int i = tempList.Count - 1; i >= 0; i--)
                            {
                                if (buyingFromIndexes.Last() + 1 == result.Count)
                                {
                                    result.Add(tempList[i]);
                                }
                                else
                                {
                                    result.Insert(buyingFromIndexes.Last() + 1, tempList[i]);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < tempList.Count; i++)
                            {
                                result.Insert(sellingToIndexes.First(), tempList[i]);
                            }
                        }
                    }

                    if (delete_list)
                    {
                        temp.RemoveAt(j);
                        j -= 1;
                    }

                    // Check if the sell sector is already in the result list
                    sellingToIndexes = LocationOfSectorInRoute(trade.BuyOffer.Buyer.HomeSector, result);
                    buyingFromIndexes = LocationOfSectorInRoute(trade.SellOffer.Seller.HomeSector, result);
                }


                // Both sectors are already in the result list
                if (sellingToIndexes.Count > 0 && buyingFromIndexes.Count > 0)
                {
                    // If they are already present in the correct order, just drop them in
                    if (sellingToIndexes.Last() > buyingFromIndexes.First())
                    {
                        result.Insert(sellingToIndexes.Last(), sellingTo_offer);
                        result.Insert(buyingFromIndexes.First(), buyingFrom_offer);
                    }

                    // They are in the wrong order, append sell to the end
                    if (sellingToIndexes.Last() < buyingFromIndexes.First())
                    {
                        result.Insert(buyingFromIndexes.First(), buyingFrom_offer);
                        result.Add(sellingTo_offer);
                    }

                    // Equal indices - put the buy order first
                    if (sellingToIndexes.Last() == buyingFromIndexes.First())
                    {
                        result.Insert(sellingToIndexes.Last(), sellingTo_offer);
                        result.Insert(sellingToIndexes.Last(), buyingFrom_offer);
                    }
                }
                else if (sellingToIndexes.Count > 0)
                {
                    // Only the sell order is in the list - add the sell order at the right-most 
                    // occurrence and add the buy order before the leftmost
                    result.Insert(sellingToIndexes.Last(), sellingTo_offer);
                    result.Insert(sellingToIndexes.First(), buyingFrom_offer);
                }
                else if (buyingFromIndexes.Count > 0)
                {
                    // Only the buy order is in the list. Add the buy order at that position, and add sell order at the end
                    result.Insert(buyingFromIndexes.First(), buyingFrom_offer);
                    result.Add(sellingTo_offer);
                }
                else
                {
                    // Neither item is in the list
                    temp.Add(new List<TradeOffer>()
                    {
                        buyingFrom_offer,
                        sellingTo_offer
                    });
                }
            }

            return result;
        }

        private static List<int> LocationOfSectorInRoute(Sector sector, List<TradeOffer> route)
        {
            var indices = new List<int>();

            for (int i = 0; i < route.Count; i++)
            {
                Sector resultSector = null;

                if (route[i].Buyer != null)
                { resultSector = route[i].Buyer.HomeSector; }
                else
                { resultSector = route[i].Seller.HomeSector; }

                if (resultSector == sector)
                {
                    indices.Add(i);
                }
            }

            return indices;
        }
    }
}