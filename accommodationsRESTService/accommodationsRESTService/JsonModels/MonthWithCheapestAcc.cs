using accommodationsRESTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace accommodationsRESTService.JsonModels
{
    public class MonthWithCheapestAcc
    {
        public int Month { get; set; }

        public Accommodation CheapestAccommodation { get; set; }
    }
}
