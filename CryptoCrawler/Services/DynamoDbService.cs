using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using CryptoCrawler.Helpers;
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
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly ILogger<DynamoDbService> _logger;

        public DynamoDbService(IDynamoDBContext dynamoDBContext, ILogger<DynamoDbService> logger)
        {
            _dynamoDBContext = dynamoDBContext;
            _logger = logger;
        }

        public async Task Add(IEnumerable<Cryptocurrency> cryptocurrencies, CancellationToken ct)
        {
            try
            {
                await _dynamoDBContext.SaveManyAsync(cryptocurrencies, ct);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "MethodName", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
                throw new Exception(ex.Message);
            }
        }
    }
}
