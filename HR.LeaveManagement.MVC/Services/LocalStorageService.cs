using Hanssens.Net;
using HR.LeaveManagement.MVC.Contracts;

namespace HR.LeaveManagement.MVC.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly LocalStorage _storage;
        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "HR.LEAVEMANAGEMENT"
            };
            LocalStorage storage = new LocalStorage(config);
            _storage = storage;
        }
        public void ClearStorage(List<string> keys)
        {
            foreach (var key in keys)
            {
                _storage.Remove(key);
            }
        }

        public void ClearStorageValue(string key)
        {
            _storage.Remove(key);
        }

        public bool DoesExist(string key)
        {
            return _storage.Exists(key);
        }

        public T getStorageValue<T>(string key)
        {
            return _storage.Get<T>(key);
        }

        public void setStorageValue<T>(string key, T value)
        {
            _storage.Store<T>(key, value);
            _storage.Persist();
        }
    }
}
