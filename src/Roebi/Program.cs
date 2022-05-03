using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Roebi.Auth;
using Roebi.Common.Context;
using Roebi.Common.UnitOfWork;
using Roebi.Helper;
using Roebi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

var host = builder.Configuration["DBHOST"] ?? "10.100.204.51";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["DBPASSWORD"] ?? "development";
var db = builder.Configuration["DBNAME"] ?? "roebi";
var user = builder.Configuration["DBUSER"] ?? "roebi";
var connectionString = $"server={host}; userid={user}; pwd={password};"
        + $"port={port}; database={db};SslMode=none;allowpublickeyretrieval=True;";

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<RoebiContext>(options => options.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure()));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
