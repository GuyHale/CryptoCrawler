using CryptoCrawler;
using CryptoCrawler.Interfaces;
using CryptoCrawler.Services;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Entity.Infrastructure;
using System.Net;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((HostContext, services) =>
    {
        services
        .AddSingleton<IDapperWrapper, DapperWrapper>()
        .AddSingleton<ICryptoScraper, CryptoScraper>()
        .AddSingleton<IUpdateSql, UpdateSqlService>()
        .AddSingleton<IDbConnectionFactory, SqlConnectionFactory>()
        .AddSingleton<IAmazonDynamoClient, AmazonDynamoClient>()
        .AddSingleton<IDynamoDb, DynamoDbService>()
        .AddHostedService<Worker>();

    }).Build();

await host.RunAsync();
