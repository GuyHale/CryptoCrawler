using Amazon;
using Amazon.DynamoDBv2;
using CryptoCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Services
{
    public class AmazonDynamoClient : IAmazonDynamoClient
    {
        public AmazonDynamoDBClient CreateClient() 
        {
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // This client will access the US East 1 region.
            clientConfig.RegionEndpoint = RegionEndpoint.EUWest2;
            return new (clientConfig);
        }
    }
}
