using CryptoCrawler.Helpers;
using CryptoCrawler.Interfaces;
using CryptoCrawler.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoCrawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICryptoScraper _cryptoScraper;
        private readonly IUpdateSql _updateSql;
        private readonly IDynamoDb _dynamoDb;
        private readonly TimeSpan _scrapeInterval = TimeSpan.FromMinutes(10);

        public Worker(ILogger<Worker> loger, ICryptoScraper cryptoScraper, IUpdateSql updateSql, IDynamoDb dynamoDb)
        {
            _logger = loger;
            _cryptoScraper = cryptoScraper;
            _updateSql = updateSql;
            _dynamoDb = dynamoDb;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            bool failure = false;
            while(!cancellationToken.IsCancellationRequested && !failure)
            {
                try
                {
                    await DoWork(cancellationToken);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, nameof(ExecuteAsync));
                    failure = true;
                }
            }
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay((int)_scrapeInterval.Milliseconds, cancellationToken);
                IEnumerable<Cryptocurrency> cryptocurrencies = CollectionToCryptocurrency.CreateCryptocurrencies(_cryptoScraper.WebScraper()).OrderBy(x => x.Rank);
                await _dynamoDb.Add(cryptocurrencies, cancellationToken);
            }
        }
    }
}
