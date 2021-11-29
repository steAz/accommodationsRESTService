using accommodationsRESTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace accommodationsRESTService.JsonModels
{
    public class AllAccommodations
    {
        public List<Accommodation> Accommodations { get; set; }

        /// <summary>
        /// This method fills in dictionary to store unique months with a cheapest price and correct accommodation's Id (as tuple)
        /// </summary>
        /// <returns>Dictionary (key: unique month, value: tuple of cheapest price and accommodation's Id)</returns>
        public Dictionary<int, ValueTuple<int, int>> GetDictMonthsWithCheapestAcc()
        {
            Dictionary<int, ValueTuple<int, int>> dictMonthsWithCheapestAcc = new Dictionary<int, (int, int)>(); 

            foreach (var ac in Accommodations)
            {
                foreach (var infoAboutPrice in ac.Prices)
                {
                    int month = infoAboutPrice.Date.Month;
                    int actualPrice = infoAboutPrice.Price;
                    //Check the actual price -> 
                    // If a lower price is found, replace the current one, adding the Id of the accommodation in which the price occurs
                    if (dictMonthsWithCheapestAcc.TryGetValue(month, out ValueTuple<int, int> tuplePriceAndAccId))
                    {
                        if (actualPrice < tuplePriceAndAccId.Item1) dictMonthsWithCheapestAcc[month] = (actualPrice, ac.Id);
                    }
                    else
                    {
                        dictMonthsWithCheapestAcc[month] = (actualPrice, ac.Id);
                    }
                }
            }

            return dictMonthsWithCheapestAcc;
        }
    }
}
