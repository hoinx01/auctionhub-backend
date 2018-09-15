using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SrvCornet.Core
{
    public class AppSettings
    {
        private static IConfiguration config;
        public static void SetConfig(IConfiguration configuration)
        {
            config = configuration;
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(config.GetSection(key).Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int GetInt32(string key, int defaultValue = 0)
        {
            try
            {
                return int.Parse(config.GetSection(key).Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long GetInt64(string key, long defaultValue = 0L)
        {
            try
            {
                return Int64.Parse(config.GetSection(key).Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetString(string key, string defaultValue = "")
        {
            try
            {
                return config.GetSection(key).Value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T Get<T>(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                return config.Get<T>();
            else
                return config.GetSection(key).Get<T>();
        }
    }
}
