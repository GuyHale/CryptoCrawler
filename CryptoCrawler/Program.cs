using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using CryptoCrawler;
using CryptoCrawler.Interfaces;
using CryptoCrawler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Net;

AWSOptions aWSOptions = new Amazon.Extensions.NETCore.Setup.AWSOptions()
{
    Region = RegionEndpoint.EUWest2,
    Profile = "guy_hale_legend"
};

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(async (HostContext, services) =>
    {
        services
        .AddSingleton<IDapperWrapper, DapperWrapper>()
        .AddSingleton<ICryptoScraper, CryptoScraper>()
        .AddSingleton<IUpdateSql, UpdateSqlService>()
        .AddSingleton<IDbConnectionFactory, SqlConnectionFactory>()
        .AddSingleton<IDynamoDb, DynamoDbService>()
        .AddAWSService<IAmazonDynamoDB>()
        .AddDefaultAWSOptions(aWSOptions)
        .AddScoped<IDynamoDBContext, DynamoDBContext>()
        .AddHostedService<Worker>();

    }).Build();

await host.RunAsync();
