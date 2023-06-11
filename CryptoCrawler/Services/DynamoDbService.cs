using Amazon.DynamoDBv2;
using CryptoCrawler.Interfaces;
using CryptoCrawler.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Services
{
    public class DynamoDbService : IDynamoDb
    {
        private readonly IAmazonDynamoClient _amazonDynamoClient;
        private readonly ILogger<DynamoDbService> _logger;

        public DynamoDbService(IAmazonDynamoClient amazonDynamoClient, ILogger<DynamoDbService> logger)
        {
            _amazonDynamoClient = amazonDynamoClient;
            _logger = logger;
        }

        public async Task Add(IEnumerable<Cryptocurrency> cryptocurrencies)
        {
            AmazonDynamoDBClient amazonDynamoDBClient = _amazonDynamoClient.CreateClient();
            await Task.Delay(1);
        }
    }
}
