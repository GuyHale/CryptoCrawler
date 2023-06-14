using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCrawler.Helpers
{
    public static class DynamoDbHelper
    {
        public static async Task SaveManyAsync<T>(this IDynamoDBContext dynamoDBContext, IEnumerable<T> collection, CancellationToken ct)
        {
            foreach(T item in collection)
            {
                await dynamoDBContext.SaveAsync(item, ct);
            }
        }
    }
}
