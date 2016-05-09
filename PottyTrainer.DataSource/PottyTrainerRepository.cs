using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using PottyTrainer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PottyTrainer.DataSource
{
    public class PottyTrainerRepository : IPottyTrainerRepository
    {
        private const string EndpointUri = "https://kdinndb.documents.azure.com:443/";
        private const string PrimaryKey = "6nR1SI3s80izsavOo0qB0jfbG5O1AVUfqslUG6KaciED5WjhYZbDOQIgTVzpANF9jtPAl7FydfFOEsRp6ZGc7Q==";
        private const string Database = "pottytraining";
        private const string Collection = "pottyevents";
        private static readonly Uri CollectionUri = UriFactory.CreateDocumentCollectionUri(Database, Collection);
        private readonly DocumentClient _Client;
        public PottyTrainerRepository()
        {
            _Client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            CreateDatabaseIfNotExists(Database);
            CreateDocumentCollectionIfNotExists(Database, Collection);
        }



        public async Task<int> SaveEvent(PeePooEvent evt)
        {
            try
            {
                ResourceResponse<Document> response;
                //new
                if (evt.Id <= 0)
                    response = await _Client.CreateDocumentAsync(CollectionUri, evt);
                else //update
                    response = await _Client.UpsertDocumentAsync(CollectionUri, evt);
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Error while saving: " + response.StatusCode);
                return int.Parse(response.Resource.Id);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEvent(int id)
        {
            try
            {
                var response = await _Client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Database, Collection, id.ToString()));
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("unable to delete eventId: " + id);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public PeePooEvent GetEvent(int id)
        {
            try
            {
                var response = _Client.CreateDocumentQuery<PeePooEvent>(CollectionUri, new FeedOptions { MaxItemCount = 1 }).Where(x => x.Id.Equals(id));
                var evnt = response.FirstOrDefault();
                return evnt;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PeePooEvent> GetEvents()
        {
            try
            {
                var response = _Client.CreateDocumentQuery<PeePooEvent>(CollectionUri, new FeedOptions { MaxItemCount = 100 });
                return response.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Private methods
        private async void CreateDatabaseIfNotExists(string databaseName)
        {
            // Check to verify a database with the id=FamilyDB does not exist
            try
            {
                await _Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                    await _Client.CreateDatabaseAsync(new Database { Id = databaseName });
                else throw;
            }
        }

        private async void CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            try
            {
                await _Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    var collectionInfo = new DocumentCollection
                    {
                        Id = collectionName,
                        IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
                    };

                    // Configure collections for maximum query flexibility including string range queries.

                    // Here we create a collection with 400 RU/s.
                    await _Client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseName),
                        collectionInfo,
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion


    }
}
