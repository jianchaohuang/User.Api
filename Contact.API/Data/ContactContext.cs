using Contact.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Data
{
    public class ContactContext
    {
        private IMongoDatabase _database;
        private IMongoCollection<ContactBooks> _collection;
        private AppSettings _appSettings;
        public ContactContext(IOptionsSnapshot<AppSettings> setting)
        {
            _appSettings = setting.Value;
            var client = new MongoClient(_appSettings.MongoConnectionString);
            if(client!=null)
            {
                _database = client.GetDatabase(_appSettings.ContactDataBaseName);
            }
        }
        private void CheckAndCreateCollection(string collectionName)
        {
            var collectionList = _database.ListCollections().ToList();
            var collectionNames = new List<string>();

            collectionList.ForEach(a=> collectionNames.Append(a["name"].ToString()));
            if(!collectionNames.Contains(collectionName))
            {
                _database.CreateCollection(collectionName);
            }
        }
        public IMongoCollection<ContactBooks> ContactBooks
        {
            get
            {
                //CheckAndCreateCollection("ContactBooks");
                return _database.GetCollection<ContactBooks>("ContactBooks");
            }
        }

    }
}
