namespace WebAPIMicrosservico.Data
{
    public interface INoSqlDatabase<T>
    {
        Task Add(string containerId, T data, string id);
    }
}
