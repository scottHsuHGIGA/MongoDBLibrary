using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLibrary.Services
{
    public interface IMongoDBService
    {
        /// <summary>
        /// 取得 MongoDB
        /// </summary>
        /// <param name="dbName">資料庫 名稱</param>
        void GetDatabase(string dbName);
        /// <summary>
        /// 取得 IMongoCollection
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">資料表 名稱</param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>(string tableName);
        /// <summary>
        /// 取得 Queryable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">資料表 名稱</param>
        /// <returns></returns>
        IQueryable<T> GetQueryable<T>(string tableName);
    }
    public class MongoDBService : IMongoDBService
    {
        private static MongoClient _client;
        private static IMongoDatabase DB;
        public MongoDBService(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString);
            GetDatabase(dbName);
        }

        /// <summary>
        /// 取得 MongoDB
        /// </summary>
        /// <param name="dbName">資料庫 名稱</param>
        public void GetDatabase(string dbName)
        {
            DB = _client.GetDatabase(dbName);
        }

        /// <summary>
        /// 取得 IMongoCollection
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">資料表 名稱</param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string tableName)
        {
            return DB.GetCollection<T>(tableName);
        }

        /// <summary>
        /// 取得 Queryable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">資料表 名稱</param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable<T>(string tableName)
        {
            return GetCollection<T>(tableName).AsQueryable();
        }

    }
}
