namespace HR.LeaveManagement.MVC.Contracts
{
    /// <summary>
    /// Contract for storing JWT tokens
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILocalStorageService
    {
        public T getStorageValue<T>(string key);
        public void setStorageValue<T>(string key, T value);
        public void ClearStorageValue(string key);
        public void ClearStorage(List<string> keys);
        public bool DoesExist(string key);
    }
}
