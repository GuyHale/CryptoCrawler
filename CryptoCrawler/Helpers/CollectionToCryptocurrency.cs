using CryptoCrawler.Models;
using System.Collections.Concurrent;

namespace CryptoCrawler.Helpers
{
    public static class CollectionToCryptocurrency
    {
        public static IEnumerable<Cryptocurrency> CreateCryptocurrencies(IEnumerable<string[]> scraperResults)
        {
            ConcurrentBag<Cryptocurrency> cryptocurrencies = new();
            return cryptocurrencies.CreateCryptocurrencyInParallel(scraperResults.ToList().CollateScraperOutput());           
        }

        private static IEnumerable<string[]> CollateScraperOutput(this List<string[]> scraperResults)
        {
            for(int i = 0; i < scraperResults.First().Length; i++)
            {
                yield return new[]
                {
                    scraperResults[0][i],
                    scraperResults[1][i],
                    scraperResults[2][i],
                    scraperResults[3][i]
                };
            }
        }

        private static IEnumerable<Cryptocurrency> CreateCryptocurrencyInParallel(this ConcurrentBag<Cryptocurrency> threadSafeCollection, IEnumerable<string[]> cryptocurrencyValues)
        {
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1
            };

            Parallel.ForEach(cryptocurrencyValues, parallelOptions, crypto =>
            {
                threadSafeCollection.Add(new Cryptocurrency()
                {
                    Rank = short.Parse(crypto[0]),
                    Name = crypto[1],
                    USDValuation = crypto[2],
                    MarketCap = crypto[3]
                });
            });
            return threadSafeCollection.AsEnumerable();
        }

    }
}
