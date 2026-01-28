namespace UStack.Identity.Application.Interfaces;

public interface IInMemoryCacheStorage
{
    void SetString(string key, string value, TimeSpan? expiration = null);
    string? GetString(string key);
    void Remove(string key);
}
