using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace zah_core.Redis.Impl
{
    public class RedisCacheImpl:ICache
    {
        private readonly IDatabase _database;
        public RedisCacheImpl(IDatabase database)
        {
            _database = database;
        }

        public int TimeOut
        {
            get
            {
                return 600;//默认
            }
        }

        public T Get<T>(string key)
        {
            var cacheValue = _database.StringGet(key);
            var value = default(T);
            if (!cacheValue.IsNullOrEmpty)
            {
                value = JsonConvert.DeserializeObject<T>(cacheValue);
            }
            return value;
        }

        /// <summary>
        /// 插入Redis，永不过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Insert(string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            _database.StringSet(key, jsonData);
        }

        public void Insert(string key, object data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = JsonConvert.SerializeObject(data);
            _database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert(string key, object data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = JsonConvert.SerializeObject(data);
            _database.StringSet(key, jsonData, timeSpan);
           
        }

        public void Insert<T>(string key, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            _database.StringSet(key, jsonData);
        }

        public void Insert<T>(string key, T data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = JsonConvert.SerializeObject(data);
            _database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert<T>(string key, T data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = JsonConvert.SerializeObject(data);
            _database.StringSet(key, jsonData, timeSpan);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                    return _database.KeyExists(key);
            }
            else
            {
                return false;
            }
        }

        public void HashSet<T>(string key, string field, List<T> data)
        {
            if (data == null || data.Count < 1) return;
            var jsonData = JsonConvert.SerializeObject(data);
            HashEntry[] items = new HashEntry[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                _database.HashSet(key, field + "-" + i.ToString(), JsonConvert.SerializeObject(item));
            }
        }

        public void ListSet<T>(string key, List<T> data)
        {
            if (data == null || data.Count < 1) return;
            var jsonData = JsonConvert.SerializeObject(data);
            HashEntry[] items = new HashEntry[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                _database.ListLeftPush(key, JsonConvert.SerializeObject(item));
            }

        }

        public void ListLeftPush<T>(string key, List<T> data)
        {
            if (data == null || data.Count < 1) return;
            HashEntry[] items = new HashEntry[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                _database.ListLeftPush(key, JsonConvert.SerializeObject(item));
            }

        }

        /// <summary>
        /// 向List中添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="mode">1:left  2right</param>
        public void ListPush<T>(string key, T data, int mode = 1)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            if (mode == 1)
            {
                _database.ListLeftPush(key, jsonData);
            }
            else
            {
                _database.ListRightPush(key, jsonData);
            }
        }

        public long ListDelete(string key, string value)
        {
            return _database.ListRemove(key, value);
        }
        /// <summary>
        /// 从List取出数据并从List中移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="mode">1:right  left</param>
        /// <returns></returns>
        public T ListPop<T>(string key, int mode = 1)
        {
            DateTime begin = DateTime.Now;
            RedisValue cacheValue = RedisValue.Null;

            if (mode == 1)
            {
                cacheValue = _database.ListRightPop(key);
            }
            else
            {
                cacheValue = _database.ListLeftPop(key);
            }

            var value = default(T);
            if (!cacheValue.IsNullOrEmpty)
            {
                value = JsonConvert.DeserializeObject<T>(cacheValue);

            }
            return value;
        }

        public List<T> ListGet<T>(string key)
        {
            DateTime begin = DateTime.Now;
            var cacheValue = new RedisValue[0];
            cacheValue = _database.ListRange(key);

            var list = new List<T>();
            if (cacheValue != null)
            {
                foreach (var item in cacheValue)
                {
                    list.Add(JsonConvert.DeserializeObject<T>(item));
                }

            }
            return list;
        }

        public List<T> ListGet<T>(string key, int start, int end)
        {
            DateTime begin = DateTime.Now;
            var cacheValue = new RedisValue[0];
            cacheValue = _database.ListRange(key, start, end);

            var list = new List<T>();
            if (cacheValue != null)
            {
                foreach (var item in cacheValue)
                {
                    list.Add(JsonConvert.DeserializeObject<T>(item));
                }
            }
            return list;
        }
        public long ListLength(string key)
        {
            return _database.ListLength(key);
        }
        public bool HashDelete(string key, string field)
        {
            return _database.HashDelete(key, field);
        }



        public T HashGet<T>(string key, string field)
        {
            DateTime begin = DateTime.Now;
            RedisValue cacheValue = "";
            cacheValue = _database.HashGet(key, field);

            var value = default(T);
            if (!cacheValue.IsNullOrEmpty)
            {
                value = JsonConvert.DeserializeObject<T>(cacheValue);

            }
            return value;
        }
    }
}
