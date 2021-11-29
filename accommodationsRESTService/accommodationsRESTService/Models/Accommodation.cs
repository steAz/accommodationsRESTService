using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace accommodationsRESTService.Models
{
    public class Accommodation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<InfoAboutPrice> Prices { get; set; }
    }
}
