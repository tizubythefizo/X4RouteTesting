using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4TradePathfinder
{
    public class RoutePather2
    {
        public void FindPath(List<TradeRoute> routes)
        {
            var sectorDictionary = new Dictionary<Sector, OrderableRouteDictionaryEntry>();
            var sublistCounter = 0;

            foreach (var route in routes)
            {
                var wrappedSellOffer = new WrappedOffer { IsBuy = false, Offer = route.SellOffer, AssociatedOffer = route.BuyOffer };
                var wrappedBuyOffer = new WrappedOffer { IsBuy = true, Offer = route.BuyOffer, AssociatedOffer = route.SellOffer };

                if (sectorDictionary.ContainsKey(wrappedSellOffer.Offer.Seller.HomeSector))
                {
                    //Already contains the key and at this stage there will only be 1 list per sector, just add
                    this.InsertIntoSublist(sectorDictionary[wrappedSellOffer.Offer.Seller.HomeSector].Sublist, wrappedSellOffer);
                }
                else
                {
                    //TODO: Functionize this and its copypast down below
                    //Create new OrderableRouteDictionaryEntry and its bits, add with new sector as key...
                    var dictionaryEntry = new OrderableRouteDictionaryEntry();
                    dictionaryEntry.Sublist = new SectorSublist();
                    //Should be set to 0 in xml code
                    dictionaryEntry.Sublist.IndexOfFirstBuyOffer = -1;
                    dictionaryEntry.Sublist.OfferList = new List<WrappedOffer>();

                    //Add to dictionary
                    sectorDictionary.Add(wrappedSellOffer.Offer.Seller.HomeSector, dictionaryEntry);
                    sublistCounter++;

                    //Finally, insert the offer
                    this.InsertIntoSublist(dictionaryEntry.Sublist, wrappedSellOffer);
                }

                if (sectorDictionary.ContainsKey(wrappedBuyOffer.Offer.Buyer.HomeSector))
                {
                    this.InsertIntoSublist(sectorDictionary[wrappedBuyOffer.Offer.Buyer.HomeSector].Sublist, wrappedBuyOffer);
                }
                else
                {
                    //TODO: Functionize this and its copypast down below
                    //Create new OrderableRouteDictionaryEntry and its bits, add with new sector as key...
                    var dictionaryEntry = new OrderableRouteDictionaryEntry();
                    dictionaryEntry.Sublist = new SectorSublist();
                    //Should be set to 0 in xml code
                    dictionaryEntry.Sublist.IndexOfFirstBuyOffer = -1;
                    dictionaryEntry.Sublist.OfferList = new List<WrappedOffer>();

                    //Add to dictionary
                    sectorDictionary.Add(wrappedBuyOffer.Offer.Buyer.HomeSector, dictionaryEntry);
                    sublistCounter++;

                    //Finally, insert the offer
                    this.InsertIntoSublist(dictionaryEntry.Sublist, wrappedBuyOffer);
                }
            }

            
        }

        //This should have all sells before buys when all is said and done.
        private void InsertIntoSublist(SectorSublist sublist, WrappedOffer offer)
        {
            //Can get rid of looping by having the ordered dictionary store more data, such as index of first buy offer.
            //That would get pushed back (if there is one, -1 if isn't one) every time a sell is added, but not when a buy is added.

            if (sublist.OfferList.Count == 0)
            {
                if (offer.IsBuy)
                {
                    //This'll be a 1 in XML code
                    sublist.IndexOfFirstBuyOffer = 0;
                }

                sublist.OfferList.Add(offer);
            }
            else
            {
                if(offer.IsBuy)
                {
                    //Check against 0 in xml code
                    if (sublist.IndexOfFirstBuyOffer > -1)
                    {
                        //Buy offer exists, just insert there.
                        sublist.OfferList.Insert(sublist.IndexOfFirstBuyOffer, offer);
                    }
                    else
                    {
                        //No buy offer yet, need to add it and track it.
                        sublist.OfferList.Add(offer);
                        //Don't do -1 in XML code since those are 1 based arrays.
                        sublist.IndexOfFirstBuyOffer = sublist.OfferList.Count - 1;
                    }
                }
                else
                {
                    //Check against 0 in xml code
                    if (sublist.IndexOfFirstBuyOffer > -1)
                    { sublist.IndexOfFirstBuyOffer++; }

                    sublist.OfferList.Add(offer);
                }                
            }
        }
    }
}
