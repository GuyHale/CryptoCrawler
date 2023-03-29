using CryptoCrawler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Interfaces
{
    public interface IUpdateSql
    {
        Task<IEnumerable<Cryptocurrency>> Get();
        Task Add(IEnumerable<Cryptocurrency> cryptocurrencies);
        Task Update(IEnumerable<Cryptocurrency> cryptocurrencies);
    }
}
