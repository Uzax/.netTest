using Mango.Services.LoggingAPI.Data;
using Mango.Services.LoggingAPI.Messging.Subsciber;
using Mango.Services.LoggingAPI.Repository;
using Mango.Services.LoggingAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("InMemLogs");
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<ILogsRepository , LogsRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddHostedService<MessageBusSubsciber>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
