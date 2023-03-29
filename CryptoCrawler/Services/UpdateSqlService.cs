using CryptoCrawler.Interfaces;
using CryptoCrawler.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace CryptoCrawler.Services 
{ 
    public class UpdateSqlService : IUpdateSql
    {
        private readonly IDapperWrapper _dapperWrapperService;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<UpdateSqlService> _logger;
        private readonly IConfiguration _configuration;

        public UpdateSqlService(IDapperWrapper dapperWrapperService, IDbConnectionFactory dbConnectionFactory, ILogger<UpdateSqlService> logger, IConfiguration configuration)
        {
            _dapperWrapperService = dapperWrapperService;
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Add(IEnumerable<Cryptocurrency> cryptocurrencies)
        {
            string connectionString = _configuration.GetConnectionString("CryptoScraperDb") ?? string.Empty;
            using IDbConnection connection = _dbConnectionFactory.CreateConnection(connectionString);
            try
            {
                foreach (Cryptocurrency cryptocurrency in cryptocurrencies)
                {
                    DynamicParameters parameters = new();
                    parameters.Add("_id", cryptocurrency.Rank, DbType.Int16);
                    parameters.Add("_rank", cryptocurrency.Rank, DbType.Int16);
                    parameters.Add("_name", cryptocurrency.Name, DbType.String);
                    parameters.Add("_USDValuation", cryptocurrency.USDValuation, DbType.String);
                    parameters.Add("_marketCap", cryptocurrency.MarketCap, DbType.String);
                    parameters.Add("_abbreviation", cryptocurrency.Abbreviation ?? string.Empty, DbType.String);
                    parameters.Add("_description", cryptocurrency.Description ?? string.Empty, DbType.String);
                    await _dapperWrapperService.QuerySinglOrDefaultAsync<Cryptocurrency>(connection, "AddCryptocurrency", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", nameof(Update));
            }
        }

        public async Task Update(IEnumerable<Cryptocurrency> cryptocurrencies)
        {
            string connectionString = _configuration.GetConnectionString("CryptoScraperDb") ?? string.Empty;
            using IDbConnection connection = _dbConnectionFactory.CreateConnection(connectionString);
            try
            {
                foreach (Cryptocurrency cryptocurrency in cryptocurrencies)
                {
                    DynamicParameters parameters = new();
                    parameters.Add("_rank", cryptocurrency.Rank, DbType.Int16);
                    parameters.Add("_name", cryptocurrency.Name, DbType.String);
                    parameters.Add("_coin_value", cryptocurrency.USDValuation, DbType.String);
                    parameters.Add("_market_cap", cryptocurrency.MarketCap, DbType.String);
                    parameters.Add("_description", cryptocurrency.Description ?? string.Empty, DbType.String);
                    await _dapperWrapperService.QuerySinglOrDefaultAsync<Cryptocurrency>(connection, "UpdateCryptocurrency", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}",nameof(Update));
            }
        }
    }
}
