using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Models
{
    public class Cryptocurrency
    {
        public short Rank { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public string? USDValuation { get; set; }
        public string? MarketCap { get; set; }
        public string? Description { get; set; }

    }
}
