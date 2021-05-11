using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Domain.Base
{
    public abstract class MongoRepositoryBase<TDocument> : IMongoRepositoryBase<TDocument>
        where TDocument : IDocument
    {
        #region Contructor

        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepositoryBase(IMongoDBSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        #endregion Contructor

        #region Sync

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void DeleteMany(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public TDocument FindById(string id)
        {
            throw new NotImplementedException();
        }

        public TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public void InsertOne(TDocument document)
        {
            throw new NotImplementedException();
        }

        public void ReplaceOne(TDocument document)
        {
            throw new NotImplementedException();
        }

        #endregion Sync

        #region Async

        public Task DeleteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindSync(filterExpression).FirstOrDefaultAsync());
        }

        public Task InsertManyAsync(ICollection<TDocument> documents)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public Task ReplaceOneAsync(TDocument document)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).ToEnumerable());
        }

        #endregion Async
    }
}