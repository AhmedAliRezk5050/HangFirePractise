using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using HangFirePractise.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IServiceManagement, ServiceManagement>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire");
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IServiceManagement>(x =>
    x.SyncData(), "0 * * ? * *");

app.Run();