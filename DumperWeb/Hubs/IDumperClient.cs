namespace DumperWeb.Hubs
{
    public interface IDumperClient
    {
        Task GetImageAsync(string message);
    }
}
