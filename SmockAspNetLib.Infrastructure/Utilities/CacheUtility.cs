//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Web;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    /// <summary>
//    /// 缓存通用类
//    /// </summary>
//    public class CacheUtility
//    {
//        //TODO:清空缓存，删除指定缓存，从缓存中得到数据
//    }


//    public class CacheManager
//    {
//        static private CacheManager _default;
//        /// <summary>
//        /// Default cache manager, with 100ms poll time, 30 seconds max wait time
//        /// </summary>
//        static public CacheManager Default
//        {
//            get
//            {
//                if (_default == null) _default = new CacheManager("_ky", 100, 30 * 1000);
//                return _default;
//            }
//        }

//        public string CacheKeyPrefix { get; set; }
//        int WaitTickIntervalInMilliseconds { get; set; }
//        int MaxWaitTimeInMilliSeconds { get; set; }

//        /// <summary>
//        /// Creates a new cache manager
//        /// </summary>
//        /// <param name="cacheKeyPrefix">prefix to add to cached item keys</param>
//        /// <param name="waitTickIntervalInMilliseconds">Interval in milliseconds to poll if a cached item is loaded in multithreaded environments</param>
//        /// <param name="maxWaitTimeInMilliSeconds">Maximum wait time in milliseconds for a cached item to load.</param>
//        public CacheManager(string cacheKeyPrefix, int waitTickIntervalInMilliseconds, int maxWaitTimeInMilliSeconds)
//        {
//            this.CacheKeyPrefix = cacheKeyPrefix;
//            this.WaitTickIntervalInMilliseconds = waitTickIntervalInMilliseconds;
//            this.MaxWaitTimeInMilliSeconds = maxWaitTimeInMilliSeconds;
//        }

//        /// <summary>
//        /// Remove all cached items starting with prefix
//        /// </summary>
//        /// <param name="keyPrefix">if null, all cached items will be removed</param>
//        public void Remove(string keyPrefix)
//        {
//            keyPrefix = this.CacheKeyPrefix + keyPrefix;
//            var keysToRemove = new List<string>();
//            var cacheEnumerator = HttpRuntime.Cache.GetEnumerator();
//            while (cacheEnumerator.MoveNext())
//            {
//                if (string.IsNullOrEmpty(keyPrefix) || cacheEnumerator.Key.ToString().StartsWith(keyPrefix))
//                {
//                    keysToRemove.Add(cacheEnumerator.Key.ToString());
//                }
//            }
//            foreach (var key in keysToRemove)
//            {
//                HttpRuntime.Cache.Remove(key);
//            }
//        }

//        /// <summary>
//        /// Caches the result of a function, updates only when expired
//        /// </summary>
//        /// <typeparam name="T">Return type of the function</typeparam>
//        /// <param name="key">Key for cache</param>
//        /// <param name="cacheDurationSeconds">Cache duration as seconds</param>
//        /// <param name="method">Delegate for cache function</param>
//        /// <returns></returns>
//        public T CachedCall<T>(string key, int cacheDurationSeconds, Func<T> method)
//        {
//            return CachedCall(key, TimeSpan.FromSeconds(cacheDurationSeconds), method);
//        }

//        /// <summary>
//        /// Caches the result of a function, updates only when expired
//        /// </summary>
//        /// <typeparam name="T">Return type of the function</typeparam>
//        /// <param name="key">Key for cache</param>
//        /// <param name="cacheDuration">Cache duration</param>
//        /// <param name="method">Delegate for cache function</param>
//        /// <returns></returns>
//        public T CachedCall<T>(string key, TimeSpan cacheDuration, Func<T> method)
//        {
//            key = CacheKeyPrefix + key;

//            var cached = HttpRuntime.Cache[key] as CachedObject<T>;
//            if (cached != null)
//            {
//                if (!cached.IsLoaded)
//                {
//                    int threadWaitTicks = 1;
//                    while (!cached.IsLoaded)
//                    {
//                        // wait until load is complete
//                        Thread.Sleep(WaitTickIntervalInMilliseconds);
//                        if ((threadWaitTicks++ * WaitTickIntervalInMilliseconds) > MaxWaitTimeInMilliSeconds)
//                        {
//                            // if something goes wrong don't block
//                            // the thread forever, just return null
//                            return default(T);
//                        }
//                    }
//                    return cached.Data;
//                }

//                if (cached.IsExpired && !cached.IsLoading)
//                {
//                    cached.IsLoading = true;
//                    method.BeginInvoke(x =>
//                    {
//                        var r = method.EndInvoke(x);
//                        if (r != null)
//                        {
//                            AddToCache(key, cacheDuration, r);
//                        }
//                        else
//                        {
//                            cached.IsLoading = false;
//                        }
//                    }, null);
//                }

//                return cached.Data;
//            }
//            else
//            {
//                cached = AddToCache<T>(key, cacheDuration, default(T));
//                cached.Data = method.Invoke();
//                cached.IsLoaded = true;
//                return cached.Data;
//            }
//        }

//        static CachedObject<T> AddToCache<T>(string key, TimeSpan duration, T data)
//        {
//            // keep the object in cache twice the duration to be able to
//            // serve the last cached copy while it is being reloaded
//            var actualCacheDuration = duration.Add(duration);
//            var obj = new CachedObject<T>(data, duration);
//            HttpRuntime.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, actualCacheDuration);
//            return obj;
//        }

//        class CachedObject<T>
//        {
//            public T Data { get; set; }
//            public DateTime CacheDate { get; private set; }
//            public DateTime ExpireDate { get; set; }
//            public bool IsLoading { get; set; }
//            public bool IsLoaded { get; set; }

//            public bool IsExpired
//            {
//                get
//                {
//                    return ExpireDate < DateTime.Now;
//                }
//            }

//            public CachedObject(T data, TimeSpan cacheDuration)
//            {
//                this.Data = data;
//                this.CacheDate = DateTime.Now;
//                this.ExpireDate = this.CacheDate.Add(cacheDuration);
//                this.IsLoaded = this.Data != null;
//            }
//        }
//    }
//}
