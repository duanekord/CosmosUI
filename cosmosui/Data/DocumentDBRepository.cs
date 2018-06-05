using cosmosui.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace cosmosui.Data
{
    public static class DocumentDBRepository<T> where T : class
    {
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
        private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];
        private static DocumentClient client;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"], null, ConsistencyLevel.BoundedStaleness);

            //Commented out - creation code still exists in Region at the end of this class.
            //CreateDatabaseIfNotExistsAsync().Wait();
            //CreateCollectionIfNotExistsAsync().Wait();
        }

        public static async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            DocumentDBRepository<FieldMasterInfo>.Initialize();
            var option = new FeedOptions { EnableCrossPartitionQuery = true };

            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), option)
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }


        //Need to pass tenant as a paramter inorder to add FeedOptions PartitionKey.
        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string tenant = null)
        {
            DocumentDBRepository<FieldMasterInfo>.Initialize();
            List<T> results = new List<T>();

            if (tenant == null)
            {
                var options = new FeedOptions { EnableCrossPartitionQuery = true };

                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), options)
                    .Where(predicate)
                    .AsDocumentQuery();

                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }
            }
            if (tenant != null)
            {
                var options = new FeedOptions { PartitionKey = new PartitionKey(tenant) };

                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), options)
                    .Where(predicate)
                    .AsDocumentQuery();

                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }
            }
            return results;
        }

        public static async Task<HttpStatusCode> UpdateSingleDoc(FieldMasterInfo fmi)
        {
            DocumentDBRepository<FieldMasterInfo>.Initialize();

            var options = new RequestOptions { PartitionKey = new PartitionKey(fmi.TenantId) };
            try
            {

                Uri docURI = UriFactory.CreateDocumentUri(DatabaseId, CollectionId, fmi.id);
                var document = await client.ReadDocumentAsync<FieldMasterInfo>(docURI, options);
                var updatedDoc = await client.ReplaceDocumentAsync(docURI, fmi);

                return updatedDoc.StatusCode;

            }
            catch (DocumentQueryException e)
            {
                return e.StatusCode.Value;
            }
        }

        #region Commented out code
        //private static async Task CreateDatabaseIfNotExistsAsync()
        //{
        //    try
        //    {
        //        await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
        //    }
        //    catch (DocumentClientException e)
        //    {
        //        if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        {
        //            await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //private static async Task CreateCollectionIfNotExistsAsync()
        //{
        //    try
        //    {
        //        await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
        //    }
        //    catch (DocumentClientException e)
        //    {
        //        if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        {
        //            await client.CreateDocumentCollectionAsync(
        //                UriFactory.CreateDatabaseUri(DatabaseId),
        //                new DocumentCollection { Id = CollectionId },
        //                new RequestOptions { OfferThroughput = 1000 });
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}
        #endregion

    }
}