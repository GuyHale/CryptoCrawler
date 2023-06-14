using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Models
{
    [DynamoDBTable("Cryptocurrencies")]
    public class Cryptocurrency
    {
        [DynamoDBHashKey("Rank")]
        public short Rank { get; set; }

        [DynamoDBProperty("Name")]
        public string? Name { get; set; }

        [DynamoDBProperty("Abbreviation")]
        public string? Abbreviation { get; set; }

        [DynamoDBProperty("USDValuation")]
        public string? USDValuation { get; set; }

        [DynamoDBProperty("MarketCap")]
        public string? MarketCap { get; set; }

        [DynamoDBProperty("Description")]
        public string? Description { get; set; }

    }
}
