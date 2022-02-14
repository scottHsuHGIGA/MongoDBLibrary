using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLibrary.Extensions
{
    public static class MongoExtension
    {
        public static void Add<T>(this IMongoCollection<T> collection, T data)
        {
            if (data == null)
            {
                return;
            }
            collection.InsertOne(data);
        }
        public static void AddRange<T>(this IMongoCollection<T> collection, IEnumerable<T> datas)
        {
            if (datas == null || !datas.Any())
            {
                return;
            }
            collection.InsertMany(datas);
        }
        public static void Update<T>(this IMongoCollection<T> collection, T data)
        {
            var filter = GetFilterDefinition(data);
            if (filter == null)
            {
                return;
            }
            collection.ReplaceOne(filter, data);
        }
        public static void UpdateRange<T>(this IMongoCollection<T> collection, IEnumerable<T> datas)
        {
            if (datas == null || !datas.Any())
            {
                return;
            }
            foreach (var item in datas)
            {
                collection.Update(item);
            }
        }
        public static void Remove<T>(this IMongoCollection<T> collection, T data)
        {

            var filter = GetFilterDefinition(data);
            if (filter == null)
            {
                return;
            }
            collection.DeleteOne(filter);
        }
        public static void RemoveRange<T>(this IMongoCollection<T> collection, IEnumerable<T> datas)
        {
            if (datas == null || !datas.Any())
            {
                return;
            }
            FilterDefinition<T> filters = GetFilterDefinition(datas);
            if (filters == null)
            {
                return;
            }
            collection.DeleteMany(filters);
        }
        /// <summary>
        /// 依 模型 取得 BsonId 的 FilterDefinition
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static FilterDefinition<T> GetFilterDefinition<T>(T data)
        {
            if (data == null)
            {
                return null;
            }
            foreach (var item in typeof(T).GetProperties())
            {
                if (item?.CustomAttributes.FirstOrDefault()?.AttributeType?.Name == "BsonIdAttribute")
                {
                    return Builders<T>.Filter.Eq(item.Name, item.GetValue(data));
                }
            }
            return null;
        }
        /// <summary>
        /// 依 模型 將各 BsonId 的 FilterDefinition 用or 串聯起來
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        private static FilterDefinition<T> GetFilterDefinition<T>(IEnumerable<T> datas)
        {
            if (datas == null || !datas.Any())
            {
                return null;
            }
            FilterDefinition<T> filters = null;
            foreach (var item in datas)
            {
                var filter = GetFilterDefinition(item);
                if (filter == null)
                {
                    continue;
                }
                if (filters == null)
                {
                    filters = filter;
                }
                else
                {
                    filters = filters | filter;
                }
            }
            return filters;
        }
    }
}
