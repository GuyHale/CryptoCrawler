using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Interfaces
{
    public interface IAmazonDynamoClient
    {
        AmazonDynamoDBClient CreateClient();
    }
}