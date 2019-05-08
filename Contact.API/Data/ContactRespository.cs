using Contact.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Data
{
    public class ContactRespository: IContactRespository
    {
        private readonly ContactContext _contactContext;
        public ContactRespository(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }
        public async Task AddContactBook(ContactBooks contact)
        {
            await _contactContext.ContactBooks.InsertOneAsync(contact);
        }

        public async Task<UpdateResult> UpdateAsync()
        {
            try
            {
                //修改条件
                FilterDefinition<ContactBooks> filter = Builders<ContactBooks>.Filter.Eq("_id", new ObjectId("5bf61bfa51dabebdda770066")) 
                    & Builders<ContactBooks>.Filter.Where(d => d.Contacts.Any(a=>a.Name== "菜哥"));
                FilterDefinition<ContactBooks> filter1 = Builders<ContactBooks>.Filter.And(Builders<ContactBooks>.Filter.Eq("_id", new ObjectId("5bf61bfa51dabebdda770066"))
                    , Builders<ContactBooks>.Filter.ElemMatch(d => d.Contacts,a=>a.Name == "菜哥"));
                //要修改的字段
                //var update=Builders<ContactBooks>.Update.Set(a=>a.Contacts[-1].Title, "洗厕所");
                var update = Builders<ContactBooks>.Update.Set("Contacts.$.Title", "洗厕所");
                return await _contactContext.ContactBooks.UpdateManyAsync(filter1, update);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
