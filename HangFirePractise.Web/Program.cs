using Hangfire;
using Hangfire.Storage.SQLite;
using HangFirePractise.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSQLiteStorage(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IServiceManagement, ServiceManagement>();

var app = builder.Build();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IServiceManagement>(x =>
    x.SyncData(), "0 * * ? * *");

app.Run();