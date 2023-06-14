using CryptoCrawler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Interfaces
{
    public interface IDynamoDb
    {
        Task Add(IEnumerable<Cryptocurrency> cryptocurrencies, CancellationToken ct);
    }
}
