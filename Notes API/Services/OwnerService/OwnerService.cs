using MongoDB.Bson;
using MongoDB.Driver;
using Notes_API.Models;
using Notes_API.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes_API.Services.OwnerService
{
    public class OwnerService : IOwnerService
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _owners = database.GetCollection<Owner>(settings.OwnerCollectionName);
        }

        public async Task<Owner> Create(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            await _owners.InsertOneAsync(owner);
            return owner;
        }

        public async Task<Owner> Delete(Guid Id)
        {
            return await _owners.FindOneAndDeleteAsync(o => o.Id == Id);
        }

        public async Task<Owner> Get(Guid Id)
        {
            return await (await _owners.FindAsync(o => o.Id == Id)).FirstOrDefaultAsync();
        }

        public async Task<List<Owner>> GetAll()
        {
            return await (await _owners.FindAsync(n => true)).ToListAsync();
        }

        public async Task<Owner> Update(Guid Id, Owner owner)
        {
            return await _owners.FindOneAndReplaceAsync(o => o.Id == Id, owner);

        }
    }
}
