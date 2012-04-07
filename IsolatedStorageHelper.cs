namespace WinPhoneKit.Storage
{
    using System;
    using System.IO.IsolatedStorage;

    public static class IsolatedStorage
    {
        private static readonly IsolatedStorageSettings isolatedStorage = IsolatedStorageSettings.ApplicationSettings;

        public static void Add<TEntity>(string key, TEntity entity)
        {
            if (isolatedStorage.Contains(key))
            {
                isolatedStorage[key] = entity;
                return;
            }

            isolatedStorage.Add(key, entity);
        }

        public static TEntity Get<TEntity>(string key)
        {
            if (isolatedStorage.Contains(key))
                return (TEntity)isolatedStorage[key];

            return default(TEntity);
        }

        public static void Remove(string key)
        {
            if (isolatedStorage.Contains(key))
                isolatedStorage.Remove(key);
        }

    }
}