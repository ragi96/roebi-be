using Microsoft.EntityFrameworkCore;
using Roebi.Common.Context;
using Roebi.Common.UnitOfWork;
using Roebi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["DBPASSWORD"] ?? "development";
var db = builder.Configuration["DBNAME"] ?? "roebi";

string connectionString = $"server={host}; userid=root; pwd={password};"
        + $"port={port}; database={db};SslMode=none;allowpublickeyretrieval=True;";

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<RoebiContext>(options => options.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure()));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();


app.MapControllers();

app.Run();
