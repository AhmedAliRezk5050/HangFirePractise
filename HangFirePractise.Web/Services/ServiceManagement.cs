namespace HangFirePractise.Web.Services;

public class ServiceManagement : IServiceManagement
{
    public void SendEmail()
    {
        Console.WriteLine("Send Email: Long running task " +
                          $"{DateTime.Now:yyyy-M-d dddd}");
    }

    public void UpdateDatabase()
    {
        Console.WriteLine("Update Database: Long running task " +
                          $"{DateTime.Now:yyyy-M-d dddd}");
    }

    public void GenerateMerchandise()
    {
        Console.WriteLine("Generate Merchandise: Long running task " +
                          $"{DateTime.Now:yyyy-M-d dddd}");
    }

    public void SyncData()
    {
        Console.WriteLine("Sync Data: Short running task " +
                          $"{DateTime.Now:yyyy-M-d dddd}");
    }
}