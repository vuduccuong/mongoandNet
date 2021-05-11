using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Domain.Base
{
    public interface IMongoRepositoryBase<TDocument>
        where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        #region Sync

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindById(string id);

        void InsertOne(TDocument document);

        void InsertMany(ICollection<TDocument> documents);

        void ReplaceOne(TDocument document);

        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteById(string id);

        void DeleteMany(ICollection<TDocument> documents);

        #endregion Sync

        #region Async

        Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindByIdAsync(string id);

        Task InsertOneAsync(TDocument document);

        Task InsertManyAsync(ICollection<TDocument> documents);

        Task ReplaceOneAsync(TDocument document);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteByIdAsync(string id);

        Task DeleteManyAsync(ICollection<TDocument> documents);

        #endregion Async
    }
}