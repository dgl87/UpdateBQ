using Google.Apis.Auth.OAuth2;
using Google.Cloud.BigQuery.V2;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UpdateBQ.Data.Dtos;
using UpdateBQ.Models;

namespace UpdateBQ
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Mongo
            //string connectionString = "mongodb://usr_journey_luis_dev:s3YRq57ZmAd86La@domongofba1dev.direct.one:33445,domongofba2dev.direct.one:33446,domongofba3dev.direct.one:33447/journey_luis_dev?replicaSet=rsAzureDev&connectTimeoutMS=300000&authSource=admin&connect=replicaSet";
            //string databaseName = "journey_luis_dev";
            string connectionString = "mongodb://dpaiva:l6lA%23*e59C%23T*YaR@rsvvh1.d1.cx:19582,rsvvh2.d1.cx:23185,rsvvh3.d1.cx:32451/admin?replicaSet=rsAzureVVHPrdd1&readPreference=primary&connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-1";
            string databaseName = "journey_viavarejoholding_prod";
            //string shootingCollection = "shooting";
            string contextCollection = "context";
            //BQ
            string projectId = "journey-event-process-prod";
            var jsonPath = @"C:\credentials\journey-event-process-prod-cd7958e50f14.json";

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            //var collectionShooting = db.GetCollection<ShootingModel>(shootingCollection);
            var collectionContext = db.GetCollection<ContextModel>(contextCollection);

            int cont = 0;

            var shootings = new List<ShootingModel>();
            var indexes = new List<UpdateBigQueryDto>();

            var startDate = new DateTime(2022, 07, 15, 12, 00, 00);
            var endDate = new DateTime(2022, 07, 15, 12, 30, 00); 

            var dateQuery = new BsonDocument
            {
                { "created", new BsonDocument {
                    {"$gte", new BsonDateTime(startDate)},
                    {"$lt", new BsonDateTime(endDate)}
                }}
            };

            var contexts = collectionContext.Find(dateQuery).Sort(Builders<ContextModel>.Sort.Ascending(a => a.Created)).ToList();

            foreach (var context in contexts)
            {
                indexes.Add(new UpdateBigQueryDto
                {
                    Id = context.Id,
                    Properties = context.Properties
                });
                var credentials = GoogleCredential.FromFile(jsonPath);
                var clientBQ = BigQueryClient.Create(projectId, credentials);
                
                string i = null, z = null;

                foreach (KeyValuePair<string, string> item in context.Properties)
                {
                    i = i + $"'{item.Key}', '{item.Value}'), STRUCT(";
                }

                string resposta = null;

                if (i == null)
                {
                    resposta = $"{cont} - {context.Created} - {context.Id} não possui properties";
                    Console.WriteLine(resposta);
                    LastRecord(resposta);
                    cont++;
                    continue;
                }

                z = i.Substring(0, i.Length - 9);

                string query = $"UPDATE `journey-event-process-prod.viavarejoholding.journey_event_v2` " +
                    $"SET properties = array[STRUCT({z}] " +
                    $"WHERE DATE(_PARTITIONTIME) = '2022-07-{context.Created.Day}' and journeyContextId = '{context.Id}'; ";

                var result = clientBQ.ExecuteQuery(query, null);
                resposta = $"{cont} - {context.Created} - {context.Id} gravado com sucesso";
                Console.WriteLine(resposta);
                LastRecord(resposta);
                cont++;
            }
        }
        private static void LastRecord(string status)
        {
            string path = @"C:\temp\viaVarejoProperties.txt";
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                    sw.WriteLine(status);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
//private static async Task<List<ContextModel>> GetContextForShooting(string contextId, IMongoCollection<ContextModel> contextCollection)
//{
//    //KitModel
//    var contextFilter = Builders<ContextModel>.Filter.Eq("_id", contextId);
//    var update = contextCollection.Find(contextFilter).ToList();

//    return update;
//}
