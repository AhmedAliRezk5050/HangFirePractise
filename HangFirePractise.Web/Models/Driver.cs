namespace HangFirePractise.Web.Models;

public class Driver
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int DriverNumber { get; set; }

    public int Status { get; set; }
}