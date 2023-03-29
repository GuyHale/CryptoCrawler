using CryptoCrawler.Helpers;
using CryptoCrawler.Interfaces;
using CryptoCrawler.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICryptoScraper _cryptoScraper;
        private readonly IUpdateSql _updateSql;
        private readonly TimeSpan _scrapeInterval = TimeSpan.FromMinutes(10);

        public Worker(ILogger<Worker> loger, ICryptoScraper cryptoScraper, IUpdateSql updateSql)
        {
            _logger = loger;
            _cryptoScraper = cryptoScraper;
            _updateSql = updateSql;

        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await DoWork(cancellationToken);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, nameof(ExecuteAsync));
                }
            }
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay((int)_scrapeInterval.TotalMilliseconds, cancellationToken);
                IEnumerable<Cryptocurrency> cryptocurrencies = CollectionToCryptocurrency.CreateCryptocurrencies(_cryptoScraper.WebScraper()).OrderBy(x => x.Rank);
                if(!(await _updateSql.Get()).Any()) 
                {
                    await _updateSql.Add(cryptocurrencies);
                    return;
                }
                await _updateSql.Update(cryptocurrencies);
            }
        }
    }
}
