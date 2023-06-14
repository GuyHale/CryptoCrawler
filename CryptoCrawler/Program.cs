using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
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
        .AddSingleton<IDynamoDb, DynamoDbService>()
        .AddAWSService<IAmazonDynamoDB>()
        .AddScoped<IDynamoDBContext, DynamoDBContext>()
        .AddHostedService<Worker>();

    }).Build();

await host.RunAsync();
