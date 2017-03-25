using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Web.Common.Providers.Contracts;

namespace CourseProject.Web.Common.Providers
{
    public class CacheProvider : ICacheProvider
    {
        public object GetValue(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public void InsertWithSlidingExpiration(string key, object value, int minutes)
        {
            HttpContext.Current.Cache.Insert(
                    key,
                    value,
                    null,
                    DateTime.MaxValue,
                    TimeSpan.FromMinutes(minutes));
        }

        public void InsertWithAbsoluteExpiration(string key, object value, DateTime absoluteExpiration)
        {
            HttpContext.Current.Cache.Insert(
                    key,
                    value,
                    null,
                    absoluteExpiration,
                    TimeSpan.Zero);
        }
    }
}