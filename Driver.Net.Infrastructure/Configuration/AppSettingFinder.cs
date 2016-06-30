using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Driver.Net.Infrastructure.Configuration
{
    public static class AppSettingFinder
    {
        private class DefaultAppSettingReader : IAppSettingReader
        {
            public bool CanApply { get { return true; } }

            public IEnumerable<string> AllKeys
            {
                get { return ConfigurationManager.AppSettings.AllKeys; }
            }

            public string Query(string key)
            {
                return ConfigurationManager.AppSettings[key];
            }
        }

        private static IAppSettingReader _appSettingReader;

        static AppSettingFinder()
        {
            _appSettingReader = new DefaultAppSettingReader();
        }
        public static void Initialize(IAppSettingReader appSettingReader)
        {
            if (appSettingReader.CanApply)
                _appSettingReader = appSettingReader;
        }
        public static string Query(string key)
        {
            var keys = key.Split(':');
            return Query<string>(keys[0], keys[1], null);
        }

        public static string Query(string key, string name)
        {
            return Query<string>(key, name, null);
        }
        public static T Query<T>(string key, string name)
        {
            return Query(key, name, default(T));
        }
        public static T Query<T>(string key, string name, T defaultValue)
        {
            var value = _appSettingReader.Query(string.Format("{0}:{1}", key, name));
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Query<T>(System.Configuration.Configuration configuration, string key, string name)
        {
            return Query(configuration, key, name, default(T));
        }

        public static T Query<T>(System.Configuration.Configuration configuration, string key, string name, T defaultValue)
        {
            var item = configuration.AppSettings.Settings[string.Format("{0}:{1}", key, name)];
            if (item == null || string.IsNullOrEmpty(item.Value))
                return defaultValue;
            return (T)Convert.ChangeType(item.Value, typeof(T));
        }

        public static IEnumerable<KeyValuePair<string, string>> ReadAppSettings()
        {
            return _appSettingReader.AllKeys.Select(key => new KeyValuePair<string, string>(key, _appSettingReader.Query(key)));
        }

    }
}
